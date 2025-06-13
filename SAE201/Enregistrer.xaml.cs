using System;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace SAE201
{
    public partial class Enregistrer : Window
    {
        private readonly MainWindow mainwindow_enregistrer;

        public Enregistrer(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_enregistrer = mainwindow;
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_enregistrer.Show();
            this.Close();
        }

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerCommande(true);
        }

        private void butMettreEnAttente_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerCommande(false);
        }

        private void EnregistrerCommande(bool valider)
        {
            int numemploye = int.Parse(textBoxNumEmploye.Text);
            double prix = double.Parse(textBoxPrix.Text);

            try
            {
                var sql = "INSERT INTO COMMANDE (NUMEMPLOYE, DATECOMMANDE, VALIDER, PRIXTOTAL) " +
                          "VALUES (@numEmploye, @dateCommande, @valider, @prix) RETURNING NUMCOMMANDE";

                using (var cmd = new NpgsqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("numemploye", numemploye);
                    cmd.Parameters.AddWithValue("dateCommande", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("valider", valider);
                    cmd.Parameters.AddWithValue("prix", prix);

                    // Utilise DataAccess pour exécuter l'insert et récupérer l'ID généré
                    int numCommandeInseree = DataAccess.Instance.ExecuteInsert(cmd);

                    MessageBox.Show($"Commande enregistrée avec succès. Numéro : {numCommandeInseree}",
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur base de données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}