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

        private void butAppliquer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
