using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Filtre.xaml
    /// </summary>
    public partial class Filtre : Window
    {
        public Filtre()
        {
            InitializeComponent();
        }

        private void Button_Click_Appliquer(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
