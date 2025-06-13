using SAE201.Model;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour AjouterDemandeVin.xaml
    /// </summary>
    public partial class AjouterDemandeVin : Window
    {
        public AjouterDemandeVin(int numVin)
        {
            InitializeComponent();
            textNumVin.Text = numVin.ToString();
        }

        private void butValiderDemande_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textNumClient.Text))
            {
                AjouterClient fenetreAjoutClient = new AjouterClient();
                if (fenetreAjoutClient.ShowDialog() == true)
                {
                    textNumClient.Text = fenetreAjoutClient.NouveauNumClient.ToString();
                }
                else
                {
                    MessageBox.Show("Création du client annulée.", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (!int.TryParse(textNumVin.Text, out int numVin) ||
                !int.TryParse(textNumClient.Text, out int numClient) ||
                !int.TryParse(textQuantite.Text, out int quantite) ||
                !int.TryParse(textNumEmploye.Text, out int numEmploye))
            {
                MessageBox.Show("Veuillez remplir correctement tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Demande demande = new Demande
                {
                    NumVin = numVin,
                    NumClient = numClient,
                    NumEmploye = numEmploye,
                    QuantiteDemande = quantite,
                    DateDemande = DateTime.Now,
                    Accepter = "En attente",
                    NumCommande = 1
                };

                demande.Create();

                MessageBox.Show("Demande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’ajout : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
