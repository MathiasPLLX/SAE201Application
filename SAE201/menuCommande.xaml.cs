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
        private MainWindow mainwindow_menuCommande;
        public MenuCommande(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_menuCommande = mainwindow;
            DataContext = new CommandeTableau();
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_menuCommande.Left = this.Left;
            mainwindow_menuCommande.Top = this.Top;

            mainwindow_menuCommande.Show(); // Affiche la fenêtre principale
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

        // Nouveau bouton Valider
        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            CommandeAffichage commandeSelectionnee = dgEtatCommande.SelectedItem as CommandeAffichage;
            if (commandeSelectionnee != null)
            {
                try
                {
                    // Mettre à jour la base de données
                    var commandeModel = new Model.Commande(commandeSelectionnee.NumCommande);
                    commandeModel.Read();  // Charge les données actuelles
                    commandeModel.Valider = true; // Met à true (validée)
                    commandeModel.Update(); // Sauvegarde en base de données

                    // Mettre à jour l'affichage local
                    commandeSelectionnee.CommandeValidee = true;
                    dgEtatCommande.Items.Refresh();

                    MessageBox.Show("Commande validée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la validation de la commande : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande à valider.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Nouveau bouton Mettre en attente
        private void butMettreEnAttente_Click(object sender, RoutedEventArgs e)
        {
            CommandeAffichage commandeSelectionnee = dgEtatCommande.SelectedItem as CommandeAffichage;
            if (commandeSelectionnee != null)
            {
                try
                {
                    // Mettre à jour la base de données
                    var commandeModel = new Model.Commande(commandeSelectionnee.NumCommande);
                    commandeModel.Read();  // Charge les données actuelles
                    commandeModel.Valider = false; // Met à false (en attente)
                    commandeModel.Update(); // Sauvegarde en base de données

                    // Mettre à jour l'affichage local
                    commandeSelectionnee.CommandeValidee = false;
                    dgEtatCommande.Items.Refresh();

                    MessageBox.Show("Commande mise en attente avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise en attente de la commande : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande à mettre en attente.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_menuCommande.Show();
            this.Close();
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