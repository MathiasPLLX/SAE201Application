using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Rechercher.xaml
    /// </summary>
    public partial class Rechercher : Window
    {
        private readonly MainWindow mainwindow_rechercher;

        public Rechercher(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_rechercher = mainwindow;
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Filtre(object sender, RoutedEventArgs e)
        {
            Filtre filtre = new Filtre();
            filtre.ShowDialog();
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_rechercher.Show();
            this.Close();
        }
    }
}