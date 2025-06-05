using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Modifier_Fiche_Client.xaml
    /// </summary>
    public partial class Modifier_Fiche_Client : Window
    {
        public Modifier_Fiche_Client()
        {
            InitializeComponent();
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_Appliquer(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
