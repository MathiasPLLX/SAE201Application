using Npgsql;
using SAE201.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE201
{
    public partial class commanderDemande : Window
    {
        public commanderDemande(object demande)
        {
            InitializeComponent();
            DataContext = demande;
        }

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void butCommander_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Récupération des données depuis le DataContext (objet demande)
                dynamic demande = DataContext;

                // Connexion à la base de données
                string connectionString = $"Host=localhost;Port=5432;Username={StockageIdentifiant.IdentifiantStocke};Password={StockageIdentifiant.MdpStocke};Database=SAE201BDD;Options='-c search_path=production'";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Début de la transaction pour assurer la cohérence
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Créer une nouvelle commande
                            string insertCommandeQuery = @"
                        INSERT INTO COMMANDE (NUMEMPLOYE, DATECOMMANDE, VALIDER, PRIXTOTAL) 
                        VALUES (@numEmploye, @dateCommande, @valider, @prixTotal) 
                        RETURNING NUMCOMMANDE";

                            using (var cmd = new NpgsqlCommand(insertCommandeQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@numEmploye", demande.NumEmploye);
                                cmd.Parameters.AddWithValue("@dateCommande", DateTime.Now.Date);
                                cmd.Parameters.AddWithValue("@valider", false); // Commande non validée par défaut

                                // Calculer le prix total (prix du vin * quantité demandée)
                                decimal prixVin = GetPrixVin(demande.NumVin, connection, transaction);
                                decimal prixTotal = prixVin * demande.QuantiteDemande;
                                cmd.Parameters.AddWithValue("@prixTotal", prixTotal);

                                // Récupérer le numéro de la commande générée
                                int numCommande = (int)cmd.ExecuteScalar();

                                // 2. Ajouter le détail de la commande
                                string insertDetailQuery = @"
                            INSERT INTO DETAILCOMMANDE (NUMCOMMANDE, NUMVIN, QUANTITE, PRIX) 
                            VALUES (@numCommande, @numVin, @quantite, @prix)";

                                using (var cmdDetail = new NpgsqlCommand(insertDetailQuery, connection, transaction))
                                {
                                    cmdDetail.Parameters.AddWithValue("@numCommande", numCommande);
                                    cmdDetail.Parameters.AddWithValue("@numVin", demande.NumVin);
                                    cmdDetail.Parameters.AddWithValue("@quantite", demande.QuantiteDemande);
                                    cmdDetail.Parameters.AddWithValue("@prix", prixTotal);

                                    cmdDetail.ExecuteNonQuery();
                                }

                                // 3. Mettre à jour la demande (marquer comme acceptée et lier à la commande)
                                string updateDemandeQuery = @"
                            UPDATE DEMANDE 
                            SET ACCEPTER = 'Oui', NUMCOMMANDE = @numCommande 
                            WHERE NUMDEMANDE = @numDemande";

                                using (var cmdUpdate = new NpgsqlCommand(updateDemandeQuery, connection, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@numCommande", numCommande);
                                    cmdUpdate.Parameters.AddWithValue("@numDemande", demande.NumDemande);

                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }

                            // Valider la transaction
                            transaction.Commit();

                            MessageBox.Show($"Commande créée avec succès !",
                                          "Succès",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Annuler la transaction en cas d'erreur
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création de la commande : {ex.Message}",
                               "Erreur",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        // Méthode helper pour récupérer le prix d'un vin
        private decimal GetPrixVin(int numVin, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            string query = "SELECT PRIXVIN FROM VIN WHERE NUMVIN = @numVin";
            using (var cmd = new NpgsqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@numVin", numVin);
                var result = cmd.ExecuteScalar();
                return result != null ? (decimal)result : 0;
            }
        }
    }
}
