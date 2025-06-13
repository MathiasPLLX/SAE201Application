using Npgsql;
using System.Windows;
using System.Windows.Input;

namespace SAE201
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butConnecter_Click(object sender, RoutedEventArgs e)
        {
            logiqueConnexion();
        }

        private void logiqueConnexion()
        {
            // Vérifier que les champs ne sont pas vides
            if (string.IsNullOrWhiteSpace(TextBoxIdentifiant.Text) ||
                string.IsNullOrWhiteSpace(PasswordBoxMotDePasse.Password))
            {
                MessageBox.Show("Veuillez saisir un identifiant et un mot de passe.",
                               "Champs obligatoires",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }

            // Stocker temporairement les identifiants
            string identifiant = TextBoxIdentifiant.Text;
            string motDePasse = PasswordBoxMotDePasse.Password;

            // Tenter la connexion à la base de données
            if (TesterConnexionBDD(identifiant, motDePasse))
            {
                // Connexion réussie : stocker les identifiants
                StockageIdentifiant.IdentifiantStocke = identifiant;
                StockageIdentifiant.MdpStocke = motDePasse;
                DialogResult = true;
            }
            else
            {
                // Connexion échouée : afficher un message d'erreur et vider les champs
                MessageBox.Show("Identifiant ou mot de passe incorrect.\nVeuillez réessayer.",
                               "Erreur de connexion",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);

                // Vider les champs pour une nouvelle saisie
                TextBoxIdentifiant.Text = "";
                PasswordBoxMotDePasse.Password = "";
                TextBoxIdentifiant.Focus();
            }
        }

        private bool TesterConnexionBDD(string identifiant, string motDePasse)
        {
            string connectionString = $"Host=localhost;Port=5432;Username={identifiant};Password={motDePasse};Database=SAE201BDD;Options='-c search_path=production'";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Test optionnel : exécuter une requête simple pour vérifier l'accès
                    using (var cmd = new NpgsqlCommand("SELECT 1", connection))
                    {
                        cmd.ExecuteScalar();
                    }

                    // Récupérer le rôle associé à l'identifiant (login)
                    using (var cmd = new NpgsqlCommand(@"
                        SELECT r.NOMROLE 
                        FROM EMPLOYE e 
                        JOIN ROLE r ON e.NUMROLE = r.NUMROLE 
                        WHERE e.LOGIN = @login", connection))
                    {
                        cmd.Parameters.AddWithValue("@login", identifiant);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            StockageIdentifiant.RoleStocke = result.ToString();
                        }
                        else
                        {
                            // Si aucun rôle n'est trouvé, définir une valeur par défaut
                            StockageIdentifiant.RoleStocke = "Non défini";
                        }
                    }
                }
                return true;
            }
            catch (NpgsqlException ex)
            {
                // Log de l'erreur (optionnel)
                LogError.Log(ex, $"Échec de connexion pour l'utilisateur: {identifiant}");
                return false;
            }
            catch (Exception ex)
            {
                // Log des autres erreurs
                LogError.Log(ex, $"Erreur inattendue lors de la connexion: {identifiant}");
                return false;
            }
        }

        private void PasswordBoxMotDePasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logiqueConnexion();
            }
        }

        private void TextBoxIdentifiant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Si l'utilisateur appuie sur Entrée dans le champ identifiant, déplacer le focus vers le mot de passe
                PasswordBoxMotDePasse.Focus();
            }
        }


    }

    public static class StockageIdentifiant
    {
        private static string identifiantStocke;
        private static string mdpStocke;
        private static string roleStocke;
        private static byte roleStockeByte;
        public static string IdentifiantStocke
        {
            get
            {
                return identifiantStocke;
            }

            set
            {
                identifiantStocke = value;
            }
        }

        public static string MdpStocke
        {
            get
            {
                return mdpStocke;
            }

            set
            {
                mdpStocke = value;
            }
        }

        public static string RoleStocke
        {
            get
            {
                return roleStocke;
            }

            set
            {
                roleStocke = value;
            }
        }

        public static byte RoleStockeByte
        {
            get
            {
                if (RoleStocke == "Vendeur")
                    return 1;
                else
                    return 2;
            }
        }
    }
}
