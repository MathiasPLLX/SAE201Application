using Npgsql;
using SAE201.ClassesVues;
using SAE201.Model;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Ajouter.xaml
    /// </summary>
    public partial class MenuDemande : Window
    {
        private readonly MainWindow mainwindow_ajouter;
        private DemandesTableau tableau;
        public MenuDemande(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_ajouter = mainwindow;
            this.DataContext = new DemandesTableau();
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_ajouter.Show();
            this.Close();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butRefuser_Click(object sender, RoutedEventArgs e)
        {
            DemandeAffichage demande = dgEtatDemande.SelectedItem as DemandeAffichage;
            if (demande != null)
            {
                if (UpdateDemandeStatut(demande.NumDemande, "Non"))
                {
                    demande.Accepter = "Non";
                    dgEtatDemande.Items.Refresh(); // Rafraîchir l'affichage
                    MessageBox.Show("Demande refusée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une demande.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            DemandeAffichage demande = dgEtatDemande.SelectedItem as DemandeAffichage;
            if (demande != null)
            {
                if (UpdateDemandeStatut(demande.NumDemande, "Oui"))
                {
                    demande.Accepter = "Oui";
                    dgEtatDemande.Items.Refresh(); // Rafraîchir l'affichage
                    MessageBox.Show("Demande validée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une demande.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Méthode pour mettre à jour le statut d'une demande dans la base de données
        private bool UpdateDemandeStatut(int numDemande, string nouveauStatut)
        {
            try
            {
                // Remplacez par votre chaîne de connexion
                string connectionString = $"Host=localhost;Port=5432;Username={StockageIdentifiant.IdentifiantStocke};Password={StockageIdentifiant.MdpStocke};Database=SAE201BDD;Options='-c search_path=production'";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE DEMANDE 
                SET ACCEPTER = @accepter 
                WHERE NUMDEMANDE = @numDemande";

                    using (var cmd = new NpgsqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@accepter", nouveauStatut);
                        cmd.Parameters.AddWithValue("@numDemande", numDemande);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour de la demande : {ex.Message}",
                               "Erreur",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
                return false;
            }
        }

        // Méthode optionnelle pour rafraîchir les données depuis la base
        private void RefreshDemandesFromDatabase()
        {
            try
            {
                // Cette méthode permet de recharger toutes les demandes depuis la base
                // Utile si vous voulez être sûr que l'affichage correspond exactement à la base
                var nouveauTableau = new DemandesTableau();
                this.DataContext = nouveauTableau;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du rafraîchissement des données : {ex.Message}",
                               "Erreur",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void butCommander_Click(object sender, RoutedEventArgs e)
        {
            if (dgEtatDemande.SelectedItem != null)
            {
                var demandeSelectionnee = dgEtatDemande.SelectedItem;

                var fenetreDetails = new commanderDemande(demandeSelectionnee)
                {
                    Owner = this,
                    Left = this.Left + 100,
                    Top = this.Top + 100
                };

                fenetreDetails.ShowDialog();
            }
        }
    }
}