using SAE201.Model;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour AjouterClient.xaml
    /// </summary>
    public partial class AjouterClient : Window
    {
        private int nouveauNumClient;
        
        public AjouterClient()
        {
            InitializeComponent();
        }

        public int NouveauNumClient
        {
            get
            {
                return this.nouveauNumClient;
            }

            set
            {
                this.nouveauNumClient = value;
            }
        }

        private void butValiderClient_Click(object sender, RoutedEventArgs e)
        {
            string nom = textNomClient.Text.Trim();
            string prenom = textPrenomClient.Text.Trim();
            string email = textAdresseMail.Text.Trim();

            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Client client = new Client
                {
                    NomClient = nom,
                    PrenomClient = prenom,
                    MailClient = email
                };

                client.Create();

                NouveauNumClient = client.NumClient;
                textNumClient.Text = NouveauNumClient.ToString();

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’ajout : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
