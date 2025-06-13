using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Commande : ICrud<Commande>
    {

        private int numCommande, numEmploye;
        private DateTime? dateCommande;
        private bool? valider;
        private decimal? prixTotal;
        private int? quantiteCommande;
        private string nomVin;
        private Employe employe;
        private List<DetailCommande> detailsCommandes = new List<DetailCommande>();

        // Propriétés pour l'affichage dans le DataGrid
        public string CommandeValidee
        {
            get
            {
                return Valider.HasValue ? (Valider.Value ? "Oui" : "Non") : "Non défini";
            }
        }

        public int? QuantiteCommande
        {
            get
            {
                return this.quantiteCommande;
            }

            set
            {
                this.quantiteCommande = value;
            }
        }

        public decimal? PrixTotalCommande
        {
            get
            {
                return this.PrixTotal;
            }
        }

        public string NomVin
        {
            get
            {
                return this.nomVin;
            }

            set
            {
                this.nomVin = value;
            }
        }

        // Objets liés
        public Employe Employe
        {
            get
            {
                return this.employe;
            }

            set
            {
                this.employe = value;
            }
        }

        public List<DetailCommande> DetailsCommandes
        {
            get
            {
                return this.detailsCommandes;
            }

            set
            {
                this.detailsCommandes = value;
            }
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public int NumEmploye
        {
            get
            {
                return this.numEmploye;
            }

            set
            {
                this.numEmploye = value;
            }
        }

        public DateTime? DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public bool? Valider
        {
            get
            {
                return this.valider;
            }

            set
            {
                this.valider = value;
            }
        }

        public decimal? PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

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

            foreach (DataRow row in table.Rows)
            {
                var detail = new DetailCommande
                {
                    NumCommande = Convert.ToInt32(row["NUMCOMMANDE"]),
                    NumVin = Convert.ToInt32(row["NUMVIN"]),
                    Quantite = row["QUANTITE"] == DBNull.Value ? null : Convert.ToInt32(row["QUANTITE"]),
                    Prix = row["PRIX"] == DBNull.Value ? null : Convert.ToDecimal(row["PRIX"]),
                    Vin = new Vin
                    {
                        NumVin = Convert.ToInt32(row["NUMVIN"]),
                        NomVin = row["NOMVIN"].ToString()
                    }
                };

                DetailsCommandes.Add(detail);
            }
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


}