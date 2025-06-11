using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Commande : ICrud<Commande>
    {
        public int NumCommande { get; set; }
        public int NumEmploye { get; set; }
        public DateTime? DateCommande { get; set; }
        public bool? Valider { get; set; }
        public decimal? PrixTotal { get; set; }

        // Propriétés pour l'affichage dans le DataGrid
        public string CommandeValidee => Valider.HasValue ? (Valider.Value ? "Oui" : "Non") : "Non défini";
        public int? QuantiteCommande { get; set; }
        public decimal? PrixTotalCommande => PrixTotal;
        public string NomVin { get; set; }

        // Objets liés
        public Employe Employe { get; set; }
        public List<DetailCommande> DetailsCommandes { get; set; } = new List<DetailCommande>();

        // Constructeurs
        public Commande() { }

        public Commande(int numCommande)
        {
            NumCommande = numCommande;
        }

        public Commande(int numEmploye, DateTime dateCommande, bool valider, decimal prixTotal)
        {
            NumEmploye = numEmploye;
            DateCommande = dateCommande;
            Valider = valider;
            PrixTotal = prixTotal;
        }

        public Commande(int numCommande, int numEmploye, DateTime dateCommande, bool valider, decimal prixTotal)
        {
            NumCommande = numCommande;
            NumEmploye = numEmploye;
            DateCommande = dateCommande;
            Valider = valider;
            PrixTotal = prixTotal;
        }

        public int Create()
        {
            var cmd = new NpgsqlCommand(@"INSERT INTO COMMANDE (NUMEMPLOYE, DATECOMMANDE, VALIDER, PRIXTOTAL) 
                                         VALUES (@numEmploye, @dateCommande, @valider, @prixTotal) 
                                         RETURNING NUMCOMMANDE;");

            cmd.Parameters.AddWithValue("@numEmploye", NumEmploye);
            cmd.Parameters.AddWithValue("@dateCommande", DateCommande ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@valider", Valider ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@prixTotal", PrixTotal ?? (object)DBNull.Value);

            return NumCommande = DataAccess.Instance.ExecuteInsert(cmd);
        }

        public void Read()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM COMMANDE WHERE NUMCOMMANDE = @id;");
            cmd.Parameters.AddWithValue("@id", NumCommande);
            var table = DataAccess.Instance.ExecuteSelect(cmd);

            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                NumEmploye = Convert.ToInt32(row["NUMEMPLOYE"]);
                DateCommande = row["DATECOMMANDE"] == DBNull.Value ? null : Convert.ToDateTime(row["DATECOMMANDE"]);
                Valider = row["VALIDER"] == DBNull.Value ? null : Convert.ToBoolean(row["VALIDER"]);
                PrixTotal = row["PRIXTOTAL"] == DBNull.Value ? null : Convert.ToDecimal(row["PRIXTOTAL"]);

                LoadRelatedObjects();
            }
        }

        private void LoadRelatedObjects()
        {
            // Charger l'employé
            if (NumEmploye > 0)
            {
                Employe = new Employe { NumEmploye = NumEmploye };
                var cmdEmploye = new NpgsqlCommand("SELECT * FROM EMPLOYE WHERE NUMEMPLOYE = @id;");
                cmdEmploye.Parameters.AddWithValue("@id", NumEmploye);
                var tableEmploye = DataAccess.Instance.ExecuteSelect(cmdEmploye);

                if (tableEmploye.Rows.Count > 0)
                {
                    var row = tableEmploye.Rows[0];
                    Employe.Nom = row["NOM"]?.ToString();
                    Employe.Prenom = row["PRENOM"]?.ToString();
                    Employe.Login = row["LOGIN"]?.ToString();
                    Employe.NumRole = Convert.ToInt32(row["NUMROLE"]);
                }
            }

            // Charger les détails de commande
            LoadDetailsCommande();
        }

        private void LoadDetailsCommande()
        {
            var cmd = new NpgsqlCommand(@"SELECT dc.*, v.NOMVIN 
                                         FROM DETAILCOMMANDE dc 
                                         JOIN VIN v ON dc.NUMVIN = v.NUMVIN 
                                         WHERE dc.NUMCOMMANDE = @id;");
            cmd.Parameters.AddWithValue("@id", NumCommande);
            var table = DataAccess.Instance.ExecuteSelect(cmd);

            DetailsCommandes.Clear();
            int totalQuantite = 0;

            foreach (DataRow row in table.Rows)
            {
                var detail = new DetailCommande
                {
                    NumCommande = Convert.ToInt32(row["NUMCOMMANDE"]),
                    NumVin = Convert.ToInt32(row["NUMVIN"]),
                    Quantite = row["QUANTITE"] == DBNull.Value ? null : Convert.ToInt32(row["QUANTITE"]),
                    Prix = row["PRIX"] == DBNull.Value ? null : Convert.ToDecimal(row["PRIX"])
                };

                DetailsCommandes.Add(detail);

                if (detail.Quantite.HasValue)
                    totalQuantite += detail.Quantite.Value;

                // Pour l'affichage, on prend le premier vin (ou on peut concaténer tous les vins)
                if (string.IsNullOrEmpty(NomVin))
                    NomVin = row["NOMVIN"]?.ToString();
            }

            QuantiteCommande = totalQuantite > 0 ? totalQuantite : null;
        }

        public int Update()
        {
            var cmd = new NpgsqlCommand(@"UPDATE COMMANDE 
                                         SET NUMEMPLOYE = @numEmploye, DATECOMMANDE = @dateCommande, 
                                             VALIDER = @valider, PRIXTOTAL = @prixTotal 
                                         WHERE NUMCOMMANDE = @id;");

            cmd.Parameters.AddWithValue("@numEmploye", NumEmploye);
            cmd.Parameters.AddWithValue("@dateCommande", DateCommande ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@valider", Valider ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@prixTotal", PrixTotal ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", NumCommande);

            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public int Delete()
        {
            var cmd = new NpgsqlCommand("DELETE FROM COMMANDE WHERE NUMCOMMANDE = @id;");
            cmd.Parameters.AddWithValue("@id", NumCommande);
            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public List<Commande> FindAll()
        {
            var cmd = new NpgsqlCommand(@"SELECT c.*, 
                                               e.NOM as NomEmploye, e.PRENOM as PrenomEmploye,
                                               SUM(dc.QUANTITE) as TotalQuantite,
                                               STRING_AGG(v.NOMVIN, ', ') as NomsVins
                                        FROM COMMANDE c
                                        LEFT JOIN EMPLOYE e ON c.NUMEMPLOYE = e.NUMEMPLOYE
                                        LEFT JOIN DETAILCOMMANDE dc ON c.NUMCOMMANDE = dc.NUMCOMMANDE
                                        LEFT JOIN VIN v ON dc.NUMVIN = v.NUMVIN
                                        GROUP BY c.NUMCOMMANDE, c.NUMEMPLOYE, c.DATECOMMANDE, c.VALIDER, c.PRIXTOTAL,
                                                 e.NOM, e.PRENOM
                                        ORDER BY c.NUMCOMMANDE;");

            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        public List<Commande> FindBySelection(string criteres)
        {
            var cmd = new NpgsqlCommand(@"SELECT c.*, 
                                               e.NOM as NomEmploye, e.PRENOM as PrenomEmploye,
                                               SUM(dc.QUANTITE) as TotalQuantite,
                                               STRING_AGG(v.NOMVIN, ', ') as NomsVins
                                        FROM COMMANDE c
                                        LEFT JOIN EMPLOYE e ON c.NUMEMPLOYE = e.NUMEMPLOYE
                                        LEFT JOIN DETAILCOMMANDE dc ON c.NUMCOMMANDE = dc.NUMCOMMANDE
                                        LEFT JOIN VIN v ON dc.NUMVIN = v.NUMVIN
                                        WHERE " + criteres + @"
                                        GROUP BY c.NUMCOMMANDE, c.NUMEMPLOYE, c.DATECOMMANDE, c.VALIDER, c.PRIXTOTAL,
                                                 e.NOM, e.PRENOM
                                        ORDER BY c.NUMCOMMANDE;");

            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        private List<Commande> ConvertToList(DataTable table)
        {
            var list = new List<Commande>();

            foreach (DataRow row in table.Rows)
            {
                var commande = new Commande
                {
                    NumCommande = Convert.ToInt32(row["NUMCOMMANDE"]),
                    NumEmploye = Convert.ToInt32(row["NUMEMPLOYE"]),
                    DateCommande = row["DATECOMMANDE"] == DBNull.Value ? null : Convert.ToDateTime(row["DATECOMMANDE"]),
                    Valider = row["VALIDER"] == DBNull.Value ? null : Convert.ToBoolean(row["VALIDER"]),
                    PrixTotal = row["PRIXTOTAL"] == DBNull.Value ? null : Convert.ToDecimal(row["PRIXTOTAL"])
                };

                // Données agrégées pour l'affichage
                if (table.Columns.Contains("TotalQuantite"))
                {
                    commande.QuantiteCommande = row["TotalQuantite"] == DBNull.Value ? null : Convert.ToInt32(row["TotalQuantite"]);
                }

                if (table.Columns.Contains("NomsVins"))
                {
                    commande.NomVin = row["NomsVins"]?.ToString();
                }

                // Informations sur l'employé
                if (table.Columns.Contains("NomEmploye"))
                {
                    commande.Employe = new Employe
                    {
                        NumEmploye = commande.NumEmploye,
                        Nom = row["NomEmploye"]?.ToString(),
                        Prenom = row["PrenomEmploye"]?.ToString()
                    };
                }

                list.Add(commande);
            }

            return list;
        }

        // Méthodes utilitaires
        public List<Commande> FindByEmploye(int numEmploye)
        {
            return FindBySelection($"c.NUMEMPLOYE = {numEmploye}");
        }

        public List<Commande> FindValidated()
        {
            return FindBySelection("c.VALIDER = true");
        }

        public List<Commande> FindNotValidated()
        {
            return FindBySelection("c.VALIDER = false");
        }

        public List<Commande> FindByDateRange(DateTime dateDebut, DateTime dateFin)
        {
            return FindBySelection($"c.DATECOMMANDE BETWEEN '{dateDebut:yyyy-MM-dd}' AND '{dateFin:yyyy-MM-dd}'");
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande && NumCommande == commande.NumCommande;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NumCommande);
        }

        public override string ToString()
        {
            string validationStatus = Valider.HasValue ? (Valider.Value ? "Validée" : "Non validée") : "Statut non défini";
            return $"Commande #{NumCommande} - Date: {DateCommande?.ToString("dd/MM/yyyy") ?? "Non définie"}, {validationStatus}, Prix: {PrixTotal?.ToString("C") ?? "Non défini"}";
        }
    }

    // Classe auxiliaire pour DetailCommande si elle n'existe pas déjà
    public class DetailCommande
    {
        public int NumCommande { get; set; }
        public int NumVin { get; set; }
        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }
    }
}