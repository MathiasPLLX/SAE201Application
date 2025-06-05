using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butConnecter_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            
        }
    }
}
