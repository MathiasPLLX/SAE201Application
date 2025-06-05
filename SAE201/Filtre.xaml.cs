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
