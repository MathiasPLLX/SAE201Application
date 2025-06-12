using SAE201.ClassesVues;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Refuser.xaml
    /// </summary>
    public partial class MenuCommande : Window
    {
        private MainWindow mainwindow_refuser;
        public MenuCommande(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_refuser = mainwindow;
            DataContext = new CommandeTableau();
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_refuser.Left = this.Left;
            mainwindow_refuser.Top = this.Top;

            mainwindow_refuser.Show(); // Affiche la fenêtre principale
            this.Close(); // Ferme la fenêtre actuelle
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgEtatCommande_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgEtatCommande.SelectedItem is CommandeAffichage commandeAffichage)
            {
                // Ouvre une fenêtre de détail (à créer) avec les vins et quantités de la commande
                var fenetreDetail = new DetailCommandeWindow(commandeAffichage.NumCommande);
                fenetreDetail.Owner = this;
                fenetreDetail.ShowDialog();
            }
        }

        private void dgEtatCommande_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Validée ?")
            {
                // On récupère l'objet CommandeAffichage sélectionné
                if (e.Row.Item is CommandeAffichage commande)
                {
                    try
                    {
                        // Récupérer la valeur "Oui"/"Non" depuis le ComboBox
                        var comboBox = e.EditingElement as ComboBox;
                        string nouvelleValeur = comboBox.SelectedItem.ToString();

                        // Convertir "Oui"/"Non" en bool
                        bool estValide = (nouvelleValeur == "Oui");

                        // Mettre à jour la base de données
                        var commandeModel = new Model.Commande(commande.NumCommande);
                        commandeModel.Read();  // Charge les données actuelles
                        commandeModel.Valider = estValide; // Modifie la valeur
                        commandeModel.Update(); // Sauvegarde en base de données

                        // Option: Actualiser la vue après mise à jour
                        RefreshDataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshDataGrid()
        {
            // Recharger les données
            DataContext = new CommandeTableau();
        }
    }
    public class BoolToOuiNonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? "Oui" : "Non";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is string s && s.Equals("Oui", StringComparison.OrdinalIgnoreCase));
        }
    }
}
