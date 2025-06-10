using SAE201.ClassesVues;
using System.Windows;
using System.Windows.Data;

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
            this.DataContext = new VinsTableau();
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_rechercher.Show();
            this.Close();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butFiltre_Click(object sender, RoutedEventArgs e)
        {
            Filtre filtre = new Filtre();
            filtre.ShowDialog();
        }

        private void textRechercher_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgVins.ItemsSource).Refresh();
        }
    }
}