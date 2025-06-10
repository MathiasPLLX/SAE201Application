using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class Vin : ICrud<Vin>
    {
        public int NumVin { get; set; }
        public int NumFournisseur { get; set; }
        public int NumType { get; set; }
        public int NumAppelation { get; set; }

        public string NomVin { get; set; }
        public decimal? PrixVin { get; set; }
        public string Descriptif { get; set; }
        public int? Millesime { get; set; }

        public Fournisseur Fournisseur { get; set; }
        public TypeVin TypeVin { get; set; }
        public Appelation Appelation { get; set; }

        public List<DetailCommande> DetailsCommandes { get; set; }
        public List<Demande> Demandes { get; set; }

        // Propriétés récuparées pour affichage dans DataGrid
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
                Appelation.NomAppelation = tableAppel.Rows[0]["nomappelation"].ToString();
            }

            TypeVin = new TypeVin { NumType = NumType };
            var cmdType = new NpgsqlCommand("SELECT * FROM TYPEVIN WHERE numtype = @id;");
            cmdType.Parameters.AddWithValue("@id", NumType);
            var tableType = DataAccess.Instance.ExecuteSelect(cmdType);
            if (tableType.Rows.Count > 0)
            {
                TypeVin.NomType = tableType.Rows[0]["nomtype"].ToString();
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