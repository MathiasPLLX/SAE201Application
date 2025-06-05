using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Ajouter.xaml
    /// </summary>
    public partial class Ajouter : Window
    {
        private readonly MainWindow mainwindow_ajouter;
        public Ajouter(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_ajouter = mainwindow;
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_ajouter.Show();
            this.Close();
        }

        private void Creer_fiche_client_Click(object sender, RoutedEventArgs e)
        {
            Creer_Fiche_Client creer_fiche_client = new Creer_Fiche_Client();
            creer_fiche_client.ShowDialog();
        }

        private void Modifier_fiche_client_Click(object sender, RoutedEventArgs e)
        {
            Modifier_Fiche_Client modifier_fiche_client = new Modifier_Fiche_Client();
            modifier_fiche_client.ShowDialog();
        }
    }
}