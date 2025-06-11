using SAE201.ClassesVues;
using SAE201.Model;
using System.Windows;
using System.Windows.Data;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Rechercher.xaml
    /// </summary>
    public partial class Rechercher : Window
    {
        private readonly MainWindow mainwindow_rechercher;
        private VinsTableau tableau;

        public Rechercher(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_rechercher = mainwindow;
            tableau = new VinsTableau();
            this.DataContext = tableau;
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_rechercher.Show();
            this.Close();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butFiltre_Click(object sender, RoutedEventArgs e)
        {
            Filtre filtre = new Filtre();
            filtre.ShowDialog();
        }

        private void filtrerDataGrid(string textRecherche)
        {

        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterVin ajouter = new AjouterVin();
            ajouter.ShowDialog();
        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var vinSelectionne = (VinAffichage)dgVins.SelectedItem;

            if (vinSelectionne != null)
            {
                var result = MessageBox.Show($"Voulez-vous vraiment supprimer le vin \"{vinSelectionne.NomVin}\" ?",
                                             "Confirmation de suppression",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Supprimer dans la base
                        var vin = new Vin { NumVin = vinSelectionne.NumVin };
                        vin.Delete();

                        // Supprimer de la liste ObservableCollection (mise à jour de l'affichage)
                        tableau.Vins.Remove(vinSelectionne);
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

        private void butActualiser_Click(object sender, RoutedEventArgs e)
        {
            tableau.Vins.Clear();

            foreach (var vin in tableau.TousLesVins())
            {
                tableau.Vins.Add(vin);
            }
        }

    }
}