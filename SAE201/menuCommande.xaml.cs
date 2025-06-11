using SAE201.ClassesVues;
using System.Windows;

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
    }
}
