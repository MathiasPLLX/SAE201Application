using SAE201.ClassesVues;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour ModifierVin.xaml
    /// </summary>
    public partial class ModifierVin : Window
    {
        private VinAffichage vinModifie;
        public ModifierVin(VinAffichage vin)
        {
            InitializeComponent();
            vinModifie = vin;

            txtNumFournisseur.Text = vin.NumFournisseur.ToString();
            txtNumType.Text = vin.NumType.ToString();
            txtNumAppelation.Text = vin.NumAppelation.ToString();
            txtNomVin.Text = vin.NomVin;
            txtPrixVin.Text = vin.PrixVin.ToString();
            txtDescriptif.Text = vin.Descriptif;
            txtMillesime.Text = vin.Millesime.ToString();
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vinModifie.NumFournisseur = int.Parse(txtNumFournisseur.Text);
                vinModifie.NumType = int.Parse(txtNumType.Text);
                vinModifie.NumAppelation = int.Parse(txtNumAppelation.Text);
                vinModifie.NomVin = txtNomVin.Text;
                vinModifie.PrixVin = decimal.Parse(txtPrixVin.Text);
                vinModifie.Descriptif = txtDescriptif.Text;
                vinModifie.Millesime = int.Parse(txtMillesime.Text);

                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Veuillez vérifier les champs saisis.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
