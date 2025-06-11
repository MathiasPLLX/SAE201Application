using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Commande : ICrud<Commande>
    {
        private int numCommande;
        private int numEmploye;
        private DateTime dateCommande;
        private bool valider;
        private decimal prixTotal;

        // Constructeur par défaut
        public Commande()
        {
        }

        // Constructeur avec ID seulement
        public Commande(int numCommande)
        {
            this.NumCommande = numCommande;
        }

        // Constructeur sans ID (pour création)
        public Commande(int numEmploye, DateTime dateCommande, bool valider, decimal prixTotal)
        {
            this.NumEmploye = numEmploye;
            this.DateCommande = dateCommande;
            this.Valider = valider;
            this.PrixTotal = prixTotal;
        }

        // Constructeur complet avec ID
        public Commande(int numCommande, int numEmploye, DateTime dateCommande, bool valider, decimal prixTotal)
        {
            this.NumCommande = numCommande;
            this.NumEmploye = numEmploye;
            this.DateCommande = dateCommande;
            this.Valider = valider;
            this.PrixTotal = prixTotal;
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
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Le numéro d'employé doit être supérieur à 0");
                }
                this.numEmploye = value;
            }
        }

        public DateTime DateCommande
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

        public bool Valider
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

        public decimal PrixTotal
        {
            get
            {
                return this.prixTotal;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Le prix total ne peut pas être négatif");
                }
                this.prixTotal = value;
            }
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("INSERT INTO COMMANDE (NUMEMPLOYE, DATECOMMANDE, VALIDER, PRIXTOTAL) VALUES (@numEmploye, @dateCommande, @valider, @prixTotal) RETURNING NUMCOMMANDE"))
            {
                cmdInsert.Parameters.AddWithValue("numEmploye", this.NumEmploye);
                cmdInsert.Parameters.AddWithValue("dateCommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("valider", this.Valider);
                cmdInsert.Parameters.AddWithValue("prixTotal", this.PrixTotal);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM COMMANDE WHERE NUMCOMMANDE = @numCommande"))
            {
                cmdSelect.Parameters.AddWithValue("numCommande", this.NumCommande);
                
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    this.NumEmploye = Convert.ToInt32(dt.Rows[0]["NUMEMPLOYE"]);
                    this.DateCommande = Convert.ToDateTime(dt.Rows[0]["DATECOMMANDE"]);
                    this.Valider = Convert.ToBoolean(dt.Rows[0]["VALIDER"]);
                    this.PrixTotal = Convert.ToDecimal(dt.Rows[0]["PRIXTOTAL"]);
                }
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("UPDATE COMMANDE SET NUMEMPLOYE = @numEmploye, DATECOMMANDE = @dateCommande, VALIDER = @valider, PRIXTOTAL = @prixTotal WHERE NUMCOMMANDE = @numCommande"))
            {
                cmdUpdate.Parameters.AddWithValue("numEmploye", this.NumEmploye);
                cmdUpdate.Parameters.AddWithValue("dateCommande", this.DateCommande);
                cmdUpdate.Parameters.AddWithValue("valider", this.Valider);
                cmdUpdate.Parameters.AddWithValue("prixTotal", this.PrixTotal);
                cmdUpdate.Parameters.AddWithValue("numCommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand("DELETE FROM COMMANDE WHERE NUMCOMMANDE = @numCommande"))
            {
                cmdDelete.Parameters.AddWithValue("numCommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM COMMANDE ORDER BY NUMCOMMANDE"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesCommandes.Add(new Commande(
                        Convert.ToInt32(dr["NUMCOMMANDE"]),
                        Convert.ToInt32(dr["NUMEMPLOYE"]),
                        Convert.ToDateTime(dr["DATECOMMANDE"]),
                        Convert.ToBoolean(dr["VALIDER"]),
                        Convert.ToDecimal(dr["PRIXTOTAL"])
                    ));
                }
            }
            return lesCommandes;
        }

        public List<Commande> FindBySelection(string criteres)
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand($"SELECT * FROM COMMANDE WHERE {criteres} ORDER BY NUMCOMMANDE"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesCommandes.Add(new Commande(
                        Convert.ToInt32(dr["NUMCOMMANDE"]),
                        Convert.ToInt32(dr["NUMEMPLOYE"]),
                        Convert.ToDateTime(dr["DATECOMMANDE"]),
                        Convert.ToBoolean(dr["VALIDER"]),
                        Convert.ToDecimal(dr["PRIXTOTAL"])
                    ));
                }
            }
            return lesCommandes;
        }

        // Méthodes utilitaires supplémentaires

        /// <summary>
        /// Trouve toutes les commandes d'un employé spécifique
        /// </summary>
        /// <param name="numEmploye">Numéro de l'employé</param>
        /// <returns>Liste des commandes de l'employé</returns>
        public List<Commande> FindByEmploye(int numEmploye)
        {
            return FindBySelection($"NUMEMPLOYE = {numEmploye}");
        }

        /// <summary>
        /// Trouve toutes les commandes validées
        /// </summary>
        /// <returns>Liste des commandes validées</returns>
        public List<Commande> FindValidated()
        {
            return FindBySelection("VALIDER = true");
        }

        /// <summary>
        /// Trouve toutes les commandes non validées
        /// </summary>
        /// <returns>Liste des commandes non validées</returns>
        public List<Commande> FindNotValidated()
        {
            return FindBySelection("VALIDER = false");
        }

        /// <summary>
        /// Trouve les commandes dans une plage de dates
        /// </summary>
        /// <param name="dateDebut">Date de début</param>
        /// <param name="dateFin">Date de fin</param>
        /// <returns>Liste des commandes dans la plage</returns>
        public List<Commande> FindByDateRange(DateTime dateDebut, DateTime dateFin)
        {
            return FindBySelection($"DATECOMMANDE BETWEEN '{dateDebut:yyyy-MM-dd}' AND '{dateFin:yyyy-MM-dd}'");
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.NumCommande == commande.NumCommande &&
                   this.NumEmploye == commande.NumEmploye &&
                   this.DateCommande == commande.DateCommande &&
                   this.Valider == commande.Valider &&
                   this.PrixTotal == commande.PrixTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NumCommande, NumEmploye, DateCommande, Valider, PrixTotal);
        }

        public override string ToString()
        {
            return $"Commande #{NumCommande} - Employé: {NumEmploye}, Date: {DateCommande:dd/MM/yyyy}, Validée: {(Valider ? "Oui" : "Non")}, Prix total: {PrixTotal:C}";
        }
    }
}