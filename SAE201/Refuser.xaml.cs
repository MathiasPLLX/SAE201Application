using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Refuser.xaml
    /// </summary>
    public partial class Refuser : Window
    {
        private readonly MainWindow mainwindow_refuser;
        public Refuser(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_refuser = mainwindow;
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            mainwindow_refuser.Show();
            this.Close();

    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
