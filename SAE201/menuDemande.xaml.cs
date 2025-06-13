using Npgsql;
using SAE201.ClassesVues;
using SAE201.Model;
using System.Windows;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Ajouter.xaml
    /// </summary>
    public partial class MenuDemande : Window
    {
        private readonly MainWindow mainwindow_ajouter;
        private DemandesTableau tableau;
        public MenuDemande(MainWindow mainwindow)
        {
            InitializeComponent();
            mainwindow_ajouter = mainwindow;
            this.DataContext = new DemandesTableau();
        }

        private void butAccueil_Click(object sender, RoutedEventArgs e)
        {
            mainwindow_ajouter.Show();
            this.Close();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butRefuser_Click(object sender, RoutedEventArgs e)
        {
            var demandeSelectionne = (DemandeAffichage)dgEtatDemande.SelectedItem;

            if (demandeSelectionne != null)
            {
                var result = MessageBox.Show($"Voulez-vous vraiment refuser la demande \"{demandeSelectionne.NumDemande}\" ?",
                                             "Confirmation de suppression",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Supprimer dans la base
                        var demande = new Demande { NumDemande = demandeSelectionne.NumDemande };
                        demande.Delete();

                        // Supprimer de la liste ObservableCollection (mise à jour de l'affichage)
                        tableau.Demandes.Remove(demandeSelectionne);
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

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            var demandeSelectionnee = (DemandeAffichage)dgEtatDemande.SelectedItem;

            if (demandeSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une demande à valider.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (demandeSelectionnee.Accepter == "Oui")
            {
                MessageBox.Show("Cette demande est déjà validée.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                // Requête SQL pour mettre à jour l'état de la demande
                string sql = "UPDATE demande SET etatdemande = 'Oui' WHERE numdemande = @num";

                using (var cmd = new NpgsqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@num", demandeSelectionnee.NumDemande);
                    int lignesAffectees = DataAccess.Instance.ExecuteSet(cmd);

                    if (lignesAffectees > 0)
                    {
                        // Met à jour localement l’objet pour que le DataGrid reflète le changement
                        demandeSelectionnee.Accepter = "Oui";

                        MessageBox.Show($"Demande \"{demandeSelectionnee.NumDemande}\" validée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Aucune ligne n’a été mise à jour. Vérifiez que la demande existe.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la validation de la demande : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void butCommander_Click(object sender, RoutedEventArgs e)
        {
            if (dgEtatDemande.SelectedItem != null)
            {
                var demandeSelectionnee = dgEtatDemande.SelectedItem;

                var fenetreDetails = new commanderDemande(demandeSelectionnee)
                {
                    Owner = this,
                    Left = this.Left + 100,
                    Top = this.Top + 100
                };

                fenetreDetails.ShowDialog();
            }
        }
    }
}