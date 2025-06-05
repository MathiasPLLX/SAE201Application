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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menu_rechercher_Click(object sender, RoutedEventArgs e)
        {
            var rechercher = new Rechercher(this);
            this.Hide();
            rechercher.ShowDialog();
        }

        private void Button_Click_deco(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var connexion = new Connexion();
            connexion.ShowDialog();
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var enrigstrer = new Enregistrer(this);
            this.Hide();
            enrigstrer.ShowDialog();
        }
    }
}