using SAE201.ClassesVues;
using SAE201.Model;
using System.Windows;
using System.Windows.Data;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Rechercher.xaml
    /// </summary>
    public partial class Rechercher : Window
    {
        private readonly MainWindow mainwindow_rechercher;
        private VinsTableau tableau;

        public Rechercher(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_rechercher = mainwindow;
            tableau = new VinsTableau();
            this.DataContext = tableau;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tableau.Vins);
            view.Filter = filtrerVin;
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_rechercher.Show();
            this.Close();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterVin ajouter = new AjouterVin();
            ajouter.ShowDialog();
        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var vinSelectionne = (VinAffichage)dgVins.SelectedItem;

            if (vinSelectionne != null)
            {
                var result = MessageBox.Show($"Voulez-vous vraiment supprimer le vin \"{vinSelectionne.NomVin}\" ?",
                                             "Confirmation de suppression",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Supprimer dans la base
                        var vin = new Vin { NumVin = vinSelectionne.NumVin };
                        vin.Delete();

                        // Supprimer de la liste ObservableCollection (mise à jour de l'affichage)
                        tableau.Vins.Remove(vinSelectionne);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un vin à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void butActualiser_Click(object sender, RoutedEventArgs e)
        {
            tableau.Vins.Clear();

            foreach (var vin in tableau.TousLesVins())
            {
                tableau.Vins.Add(vin);
            }
        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {
            var vinSelectionne = (VinAffichage)dgVins.SelectedItem;

            if (vinSelectionne == null)
            {
                MessageBox.Show(this, "Veuillez sélectionner un vin à modifier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            VinAffichage copie = new VinAffichage
            {
                NumVin = vinSelectionne.NumVin,
                NomVin = vinSelectionne.NomVin,
                PrixVin = vinSelectionne.PrixVin,
                Descriptif = vinSelectionne.Descriptif,
                Millesime = vinSelectionne.Millesime,
                NumType = vinSelectionne.NumType,
                NumAppelation = vinSelectionne.NumAppelation,
                NumFournisseur = vinSelectionne.NumFournisseur
            };

            ModifierVin modifier = new ModifierVin(copie);

            if (modifier.ShowDialog() == true)
            {
                try
                {
                    Vin vinBdd = new Vin
                    {
                        NumVin = copie.NumVin,
                        NomVin = copie.NomVin,
                        PrixVin = copie.PrixVin,
                        Descriptif = copie.Descriptif,
                        Millesime = copie.Millesime,
                        NumFournisseur = copie.NumFournisseur,
                        NumType = copie.NumType,
                        NumAppelation = copie.NumAppelation
                    };

                    vinBdd.Update();

                    // Mise à jour des données du datagrid
                    vinSelectionne.NomVin = copie.NomVin;
                    vinSelectionne.PrixVin = copie.PrixVin;
                    vinSelectionne.Descriptif = copie.Descriptif;
                    vinSelectionne.Millesime = copie.Millesime;
                    vinSelectionne.NumType = copie.NumType;
                    vinSelectionne.NumAppelation = copie.NumAppelation;
                    vinSelectionne.NumFournisseur = copie.NumFournisseur;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool filtrerVin(object obj)
        {
            if (string.IsNullOrWhiteSpace(textRechercher.Text))
                return true;

            var vin = obj as VinAffichage; //si obj ne peut pas être converti en instance de VinAffichage, vin = null
            if (vin == null) return false;

            string filtre = textRechercher.Text.ToLower();

            return vin.NomVin.ToLower().Contains(filtre)
                || vin.Descriptif?.ToLower().Contains(filtre) == true
                || vin.Millesime.ToString().Contains(filtre)
                || vin.NomAppelation?.ToLower().Contains(filtre) == true
                || vin.NomType?.ToLower().Contains(filtre) == true; //obligé de mettre des "== true" sur les nullables sinon ça marche pas
        }

        private void textRechercher_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(tableau.Vins).Refresh();
        }
    }
}