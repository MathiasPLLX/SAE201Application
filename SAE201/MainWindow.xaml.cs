using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var connexion = new Connexion();
            connexion.ShowDialog();
        }

        private void menu_ajouter_Click(object sender, RoutedEventArgs e)
        {
            var ajouter = new MenuDemande(this)
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            this.Hide();
            ajouter.ShowDialog();
        }

        private void ButtonQuitterMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            var enrigstrer = new Enregistrer(this)
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            this.Hide();
            enrigstrer.ShowDialog();
        }
        private void but_deconnecter_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var connexion = new Connexion()
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            bool? result = connexion.ShowDialog();
            if (result == true)
            {
                // Connexion réussie, on réaffiche la fenêtre principale
                this.Show();
            }
            else
            {
                // Connexion annulée ou échouée, on ferme l'application
                Application.Current.Shutdown();
            }
        }

        private void menuGestionVin_Click(object sender, RoutedEventArgs e)
        {
            var rechercher = new Rechercher(this)
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            this.Hide();
            rechercher.Show(); // Utilisez Show() au lieu de ShowDialog()
        }

        private void menuCommande_Click(object sender, RoutedEventArgs e)
        {
            Refuser refuserWindow = new Refuser(this)
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            refuserWindow.Show();
            this.Hide();
        }

        private void menuDemande_Click(object sender, RoutedEventArgs e)
        {
            MenuDemande fenetreMenuDemande = new MenuDemande(this)
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height
            };
            fenetreMenuDemande.Show();
            this.Hide();
        }
    }
}