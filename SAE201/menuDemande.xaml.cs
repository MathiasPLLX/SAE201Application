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
                demande.Accepter = "Non";
                dgEtatDemande.Items.Refresh(); // Rafraîchir l'affichage
            }
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            DemandeAffichage demande = dgEtatDemande.SelectedItem as DemandeAffichage;
            if (demande != null)
            {
                demande.Accepter = "Oui";
                dgEtatDemande.Items.Refresh(); // Rafraîchir l'affichage
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