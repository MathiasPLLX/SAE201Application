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
            var demandeSelectionne = (DemandeAffichage)dgEtatDemande.SelectedItem;

            if (demandeSelectionne != null)
            {
                var result = MessageBox.Show($"Voulez-vous vraiment refuser la demande \"{demandeSelectionne.NumDemande}\" ?",
                                             "Confirmation de suppression",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Supprimer dans la base
                        var demande = new Demande { NumDemande = demandeSelectionne.NumDemande };
                        demande.Delete();

                        // Supprimer de la liste ObservableCollection (mise à jour de l'affichage)
                        tableau.Demandes.Remove(demandeSelectionne);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un vin à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }
}