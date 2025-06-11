using Npgsql;
using SAE201.Model;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour AjouterVin.xaml
    /// </summary>
    public partial class AjouterVin : Window
    {
        public AjouterVin()
        {
            InitializeComponent();
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumFournisseur.Text))
                {
                    MessageBox.Show("Le numéro de fournisseur est obligatoire.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumFournisseur.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumType.Text))
                {
                    MessageBox.Show("Le numéro du type de vin est obligatoire.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumType.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumAppelation.Text))
                {
                    MessageBox.Show("Le numéro de l'appellation est obligatoire.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumAppelation.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNomVin.Text))
                {
                    MessageBox.Show("Le nom du vin est obligatoire.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNomVin.Focus();
                    return;
                }

                if (!int.TryParse(txtNumFournisseur.Text, out int numFournisseur))
                {
                    MessageBox.Show("Le numéro de fournisseur doit être un nombre entier.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumFournisseur.Focus();
                    return;
                }

                if (!int.TryParse(txtNumType.Text, out int numType))
                {
                    MessageBox.Show("Le numéro du type de vin doit être un nombre entier.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumType.Focus();
                    return;
                }

                if (!int.TryParse(txtNumAppelation.Text, out int numAppelation))
                {
                    MessageBox.Show("Le numéro de l'appellation doit être un nombre entier.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumAppelation.Focus();
                    return;
                }

                decimal? prixVin = null;
                if (!string.IsNullOrWhiteSpace(txtPrixVin.Text))
                {
                    if (!decimal.TryParse(txtPrixVin.Text, out decimal prix))
                    {
                        MessageBox.Show("Le prix doit être un nombre décimal valide.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtPrixVin.Focus();
                        return;
                    }
                    prixVin = prix;
                }

                int? millesime = null;
                if (!string.IsNullOrWhiteSpace(txtMillesime.Text))
                {
                    if (!int.TryParse(txtMillesime.Text, out int mill))
                    {
                        MessageBox.Show("Le millésime doit être un nombre entier valide.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtMillesime.Focus();
                        return;
                    }
                    millesime = mill;
                }

                if (!VerifierExistenceClesEtrangeres(numFournisseur, numType, numAppelation))
                {
                    return; // Le message d'erreur est déjà affiché dans la méthode de vérification
                }

                Vin nouveauVin = new Vin
                {
                    NumFournisseur = numFournisseur,
                    NumType = numType,
                    NumAppelation = numAppelation,
                    NomVin = txtNomVin.Text.Trim(),
                    PrixVin = prixVin,
                    Descriptif = string.IsNullOrWhiteSpace(txtDescriptif.Text) ? null : txtDescriptif.Text.Trim(),
                    Millesime = millesime
                };

                int numeroVinCree = nouveauVin.Create();

                if (numeroVinCree > 0)
                {
                    MessageBox.Show($"Le vin a été ajouté avec succès ! Numéro : {numeroVinCree}", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    ViderChamps();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du vin.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool VerifierExistenceClesEtrangeres(int numFournisseur, int numType, int numAppelation)
        {
            try
            {
                // Vérifier l'existence du fournisseur
                var cmdFourn = new NpgsqlCommand("SELECT COUNT(*) FROM FOURNISSEUR WHERE numfournisseur = @id;");
                cmdFourn.Parameters.AddWithValue("@id", numFournisseur);
                var resultFourn = DataAccess.Instance.ExecuteSelect(cmdFourn);
                if (Convert.ToInt32(resultFourn.Rows[0][0]) == 0)
                {
                    MessageBox.Show($"Le fournisseur n°{numFournisseur} n'existe pas.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumFournisseur.Focus();
                    return false;
                }

                // Vérifier l'existence du type
                var cmdType = new NpgsqlCommand("SELECT COUNT(*) FROM TYPEVIN WHERE numtype = @id;");
                cmdType.Parameters.AddWithValue("@id", numType);
                var resultType = DataAccess.Instance.ExecuteSelect(cmdType);
                if (Convert.ToInt32(resultType.Rows[0][0]) == 0)
                {
                    MessageBox.Show($"Le type de vin n°{numType} n'existe pas.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumType.Focus();
                    return false;
                }

                // Vérifier l'existence de l'appellation
                var cmdAppel = new NpgsqlCommand("SELECT COUNT(*) FROM APPELATION WHERE numappelation = @id;");
                cmdAppel.Parameters.AddWithValue("@id", numAppelation);
                var resultAppel = DataAccess.Instance.ExecuteSelect(cmdAppel);
                if (Convert.ToInt32(resultAppel.Rows[0][0]) == 0)
                {
                    MessageBox.Show($"L'appellation n°{numAppelation} n'existe pas.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumAppelation.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la vérification des clés étrangères : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ViderChamps()
        {
            txtNumFournisseur.Text = "";
            txtNumType.Text = "";
            txtNumAppelation.Text = "";
            txtNomVin.Text = "";
            txtPrixVin.Text = "";
            txtDescriptif.Text = "";
            txtMillesime.Text = "";
            txtNumFournisseur.Focus();
        }

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
