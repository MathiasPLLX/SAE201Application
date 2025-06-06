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

        private void menu_rechercher_Click(object sender, RoutedEventArgs e)
        {
            var rechercher = new Rechercher(this);
            this.Hide();
            rechercher.Show(); // Utilisez Show() au lieu de ShowDialog()
        }

        private void menu_ajouter_Click(object sender, RoutedEventArgs e)
        {
            var ajouter = new Ajouter(this);
            this.Hide();
            ajouter.ShowDialog();
        }

        private void menu_refuser_Click(object sender, RoutedEventArgs e)
        {
            var refuser = new Refuser(this);
            this.Hide();
            refuser.ShowDialog();
        }

        private void ButtonQuitterMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            var enrigstrer = new Enregistrer(this);
            this.Hide();
            enrigstrer.ShowDialog();
        }
        private void but_deconnecter_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var connexion = new Connexion();
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
    }
}