using SAE201.Model;
using System.Collections.Generic;
using System.Windows;

namespace SAE201
{
    public partial class DetailCommandeWindow : Window
    {
        public DetailCommandeWindow(int numCommande)
        {
            InitializeComponent();
            var commande = new Commande(numCommande);
            commande.Read();
            dgDetails.ItemsSource = commande.DetailsCommandes;
        }
    }
}