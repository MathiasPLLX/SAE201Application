using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Creer_Fiche_Client.xaml
    /// </summary>
    public partial class Creer_Fiche_Client : Window
    {
        public Creer_Fiche_Client()
        {
            InitializeComponent();
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void butAppliquer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
