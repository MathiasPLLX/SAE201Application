using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Enregistrer.xaml
    /// </summary>
    public partial class Enregistrer : Window
    {
        private readonly MainWindow mainwindow_enregistrer;
        public Enregistrer(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_enregistrer = mainwindow;
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_enregistrer.Show();
            this.Close();

    }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
