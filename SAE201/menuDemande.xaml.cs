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
            if (DataContext is DemandesTableau tableau && tableau.SelectedDemande != null)
            {
                var demandeAffichage = tableau.SelectedDemande;
                demandeAffichage.Accepter = "Non"; // Mise à jour UI

                // Mise à jour base
                var demandeModel = new Demande
                {
                    NumDemande = demandeAffichage.NumDemande,
                    NumVin = demandeAffichage.NumVin,
                    NumEmploye = demandeAffichage.NumEmploye,
                    NumCommande = demandeAffichage.NumCommande,
                    NumClient = demandeAffichage.NumClient,
                    DateDemande = demandeAffichage.DateDemande,
                    QuantiteDemande = demandeAffichage.QuantiteDemande,
                    Accepter = "Non"
                };

                int result = demandeModel.Update();
                if (result > 0)
                {
                    MessageBox.Show("Demande refusée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour en base.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une demande.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}