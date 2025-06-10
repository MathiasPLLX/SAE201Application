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

        private void butCreerFicheClient_Click(object sender, RoutedEventArgs e)
        {
            Creer_Fiche_Client creer_fiche_client = new Creer_Fiche_Client();
            creer_fiche_client.ShowDialog();
        }

        private void butModifierFicheClient_Click(object sender, RoutedEventArgs e)
        {
            Modifier_Fiche_Client modifier_fiche_client = new Modifier_Fiche_Client();
            modifier_fiche_client.ShowDialog();
        }
    }
}