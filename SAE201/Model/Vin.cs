using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Vin : ICrud<Vin>
    {
        private int numVin,numFournisseur,numType,numAppelation;
        private int? millesime;
        private string nomVin, descriptif;
        private decimal? prixVin;
        private Fournisseur fournisseur;
        private TypeVin typeVin;
        private Appelation appelation;
        private List<DetailCommande> detailsCommandes;
        private List<Demande> demandes;

        // Propriétés récuperées pour affichage dans DataGrid
        public string NomAppelation
        {
            get
            {
                if (Appelation != null)
                    return Appelation.NomAppelation;
                else
                    return "Non définie";
            }
        }

        public string NomType
        {
            get
            {
                if (TypeVin != null)
                    return TypeVin.NomType;
                else
                    return "Non défini";
            }
        }

        public int NumVin
        {
            get
            {
                return this.numVin;
            }

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value,0, "NumVin doit être supérieur à 0.");
                this.numVin = value;
            }
        }

        public int NumFournisseur
        {
            get
            {
                return this.numFournisseur;
            }

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "NumVin doit être supérieur à 0.");
                this.numFournisseur = value;
            }
        }

        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "NumVin doit être supérieur à 0.");
                this.numType = value;
            }
        }

        public int NumAppelation
        {
            get
            {
                return this.numAppelation;
            }

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "NumVin doit être supérieur à 0.");
                this.numAppelation = value;
            }
        }

        public int? Millesime
        {
            get
            {
                return this.millesime;
            }

            set
            {
                this.millesime = value;
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

        public string Descriptif
        {
            get
            {
                return this.descriptif;
            }

            set
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value, "Descriptif ne peut pas être vide ou null.");
                this.descriptif = value;
            }
        }

        public decimal? PrixVin
        {
            get
            {
                return this.prixVin;
            }

            set
            {
                if (value.HasValue && value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "PrixVin ne peut pas être négatif.");
                }
                this.prixVin = value;
            }
        }

        public Fournisseur Fournisseur
        {
            get
            {
                return this.fournisseur;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(value, "Fournisseur ne peut pas être null.");
                this.fournisseur = value;
            }
        }

        public TypeVin TypeVin
        {
            get
            {
                return this.typeVin;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(value, "TypeVin ne peut pas être null.");
                this.typeVin = value;
            }
        }

        public Appelation Appelation
        {
            get
            {
                return this.appelation;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(value, "Appelation ne peut pas être null.");
                this.appelation = value;
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
                ArgumentNullException.ThrowIfNull(value, "DetailsCommandes ne peut pas être null.");
                this.detailsCommandes = value;
            }
        }

        public List<Demande> Demandes
        {
            get
            {
                return this.demandes;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(value, "Demandes ne peut pas être null.");
                this.demandes = value;
            }
        }

        public int Create()
        {
            var cmd = new NpgsqlCommand("INSERT INTO VIN(nomvin, prixvin, descriptif, millesime, numtype, numappelation, numfournisseur) VALUES (@nom, @prix, @desc, @mill, @type, @appel, @fourn) RETURNING numvin;");
            cmd.Parameters.AddWithValue("@nom", NomVin);
            cmd.Parameters.AddWithValue("@prix", PrixVin ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@desc", Descriptif ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@mill", Millesime ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@type", NumType);
            cmd.Parameters.AddWithValue("@appel", NumAppelation);
            cmd.Parameters.AddWithValue("@fourn", NumFournisseur);

            return NumVin = DataAccess.Instance.ExecuteInsert(cmd);
        }

        public void Read()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM VIN WHERE numvin = @id;");
            cmd.Parameters.AddWithValue("@id", NumVin);
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                NomVin = row["nomvin"].ToString();
                PrixVin = row["prixvin"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["prixvin"]);
                Descriptif = row["descriptif"].ToString();
                Millesime = row["millesime"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["millesime"]);
                NumType = Convert.ToInt32(row["numtype"]);
                NumAppelation = Convert.ToInt32(row["numappelation"]);
                NumFournisseur = Convert.ToInt32(row["numfournisseur"]);

                LoadRelatedObjects();
            }
        }

        private void LoadRelatedObjects()
        {
            Appelation = new Appelation { NumAppelation = NumAppelation };
            NpgsqlCommand cmdAppel = new NpgsqlCommand($"SELECT * FROM APPELATION WHERE numappelation = {NumAppelation};");
            var tableAppel = DataAccess.Instance.ExecuteSelect(cmdAppel);
            if (tableAppel.Rows.Count > 0)
            {
                // Fix for CS0120: Use the instance of Appelation instead of referencing the class directly
                Appelation.NomAppelation = tableAppel.Rows[0]["nomappelation"]?.ToString(); // Fix for CS8601: Use null-conditional operator
            }

            TypeVin = new TypeVin { NumType = NumType };
            var cmdType = new NpgsqlCommand("SELECT * FROM TYPEVIN WHERE numtype = @id;");
            cmdType.Parameters.AddWithValue("@id", NumType);
            var tableType = DataAccess.Instance.ExecuteSelect(cmdType);
            if (tableType.Rows.Count > 0)
            {
                // Fix for CS0120: Use the instance of TypeVin instead of referencing the class directly
                TypeVin.NomType = tableType.Rows[0]["nomtype"]?.ToString(); // Fix for CS8601: Use null-conditional operator
            }
        }

        public int Update()
        {
            var cmd = new NpgsqlCommand("UPDATE VIN SET nomvin = @nom, prixvin = @prix, descriptif = @desc, millesime = @mill, numtype = @type, numappelation = @appel, numfournisseur = @fourn WHERE numvin = @id;");
            cmd.Parameters.AddWithValue("@nom", NomVin);
            cmd.Parameters.AddWithValue("@prix", PrixVin ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@desc", Descriptif ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@mill", Millesime ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@type", NumType);
            cmd.Parameters.AddWithValue("@appel", NumAppelation);
            cmd.Parameters.AddWithValue("@fourn", NumFournisseur);
            cmd.Parameters.AddWithValue("@id", NumVin);

            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public int Delete()
        {
            var cmd = new NpgsqlCommand("DELETE FROM VIN WHERE numvin = @id;");
            cmd.Parameters.AddWithValue("@id", NumVin);
            return DataAccess.Instance.ExecuteSet(cmd);
        }

        public List<Vin> FindAll()
        {
            var cmd = new NpgsqlCommand(@"SELECT v.*, a.nomappelation, t.nomtype 
                                        FROM VIN v 
                                        LEFT JOIN APPELATION a ON v.numappelation = a.numappelation 
                                        LEFT JOIN TYPEVIN t ON v.numtype = t.numtype;");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        public List<Vin> FindBySelection(string criteres)
        {
            var cmd = new NpgsqlCommand(@"SELECT v.*, a.nomappelation, t.nomtype 
                                        FROM VIN v 
                                        LEFT JOIN APPELATION a ON v.numappelation = a.numappelation 
                                        LEFT JOIN TYPEVIN t ON v.numtype = t.numtype 
                                        WHERE v.nomvin ILIKE @crit;");
            cmd.Parameters.AddWithValue("@crit", "%" + criteres + "%");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        private List<Vin> ConvertToList(DataTable table)
        {
            var list = new List<Vin>();

            foreach (DataRow row in table.Rows)
            {
                var vin = new Vin
                {
                    NumVin = Convert.ToInt32(row["numvin"]),
                    NomVin = row["nomvin"].ToString(),
                    PrixVin = row["prixvin"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["prixvin"]),
                    Descriptif = row["descriptif"].ToString(),
                    Millesime = row["millesime"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["millesime"]),
                    NumType = Convert.ToInt32(row["numtype"]),
                    NumAppelation = Convert.ToInt32(row["numappelation"]),
                    NumFournisseur = Convert.ToInt32(row["numfournisseur"])
                };

                if (table.Columns.Contains("nomappelation"))
                {
                    vin.Appelation = new Appelation
                    {
                        NumAppelation = vin.NumAppelation,
                        NomAppelation = row["nomappelation"]?.ToString()
                    };
                }

                if (table.Columns.Contains("nomtype"))
                {
                    vin.TypeVin = new TypeVin
                    {
                        NumType = vin.NumType,
                        NomType = row["nomtype"]?.ToString()
                    };
                }

                list.Add(vin);
            }

            return list;
        }
    }
}