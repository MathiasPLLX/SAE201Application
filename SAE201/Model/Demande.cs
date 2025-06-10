using Npgsql;
using System.Data;
using System.DirectoryServices.ActiveDirectory;

namespace SAE201.Model
{
    public class Demande
    {
        public int NumDemande { get; set; }

        public int NumVin { get; set; }
        public int NumEmploye { get; set; }
        public int NumCommande { get; set; }
        public int NumClient { get; set; }

        public DateTime? DateDemande { get; set; }
        public int? QuantiteDemande { get; set; }
        public string Accepter { get; set; }

        public Vin Vin { get; set; }
        public Employe Employe { get; set; }
        public Commande Commande { get; set; }
        public Client Client { get; set; }

        //        public int Create()
        //        {
        //            var cmd = new NpgsqlCommand("INSERT INTO DEMANDE(nomvin, prixvin, descriptif, millesime, numtype, numappelation, numfournisseur) VALUES (@nom, @prix, @desc, @mill, @type, @appel, @fourn) RETURNING numvin;");
        //            cmd.Parameters.AddWithValue("@nom", NomVin);
        //            cmd.Parameters.AddWithValue("@prix", PrixVin ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@desc", Descriptif ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@mill", Millesime ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@type", NumType);
        //            cmd.Parameters.AddWithValue("@appel", NumAppelation);
        //            cmd.Parameters.AddWithValue("@fourn", NumFournisseur);

        //            return NumVin = DataAccess.Instance.ExecuteInsert(cmd);
        //        }

        //        public void Read()
        //        {
        //            var cmd = new NpgsqlCommand("SELECT * FROM VIN WHERE numvin = @id;");
        //            cmd.Parameters.AddWithValue("@id", NumVin);
        //            var table = DataAccess.Instance.ExecuteSelect(cmd);
        //            if (table.Rows.Count > 0)
        //            {
        //                var row = table.Rows[0];
        //                NomVin = row["nomvin"].ToString();
        //                PrixVin = row["prixvin"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["prixvin"]);
        //                Descriptif = row["descriptif"].ToString();
        //                Millesime = row["millesime"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["millesime"]);
        //                NumType = Convert.ToInt32(row["numtype"]);
        //                NumAppelation = Convert.ToInt32(row["numappelation"]);
        //                NumFournisseur = Convert.ToInt32(row["numfournisseur"]);
        //            }
        //        }

        //        public int Update()
        //        {
        //            var cmd = new NpgsqlCommand("UPDATE VIN SET nomvin = @nom, prixvin = @prix, descriptif = @desc, millesime = @mill, numtype = @type, numappelation = @appel, numfournisseur = @fourn WHERE numvin = @id;");
        //            cmd.Parameters.AddWithValue("@nom", NomVin);
        //            cmd.Parameters.AddWithValue("@prix", PrixVin ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@desc", Descriptif ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@mill", Millesime ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@type", NumType);
        //            cmd.Parameters.AddWithValue("@appel", NumAppelation);
        //            cmd.Parameters.AddWithValue("@fourn", NumFournisseur);
        //            cmd.Parameters.AddWithValue("@id", NumVin);

        //            return DataAccess.Instance.ExecuteSet(cmd);
        //        }

        //        public int Delete()
        //        {
        //            var cmd = new NpgsqlCommand("DELETE FROM VIN WHERE numvin = @id;");
        //            cmd.Parameters.AddWithValue("@id", NumVin);
        //            return DataAccess.Instance.ExecuteSet(cmd);
        //        }

        //        public List<Vin> FindAll()
        //        {
        //            var cmd = new NpgsqlCommand("SELECT * FROM VIN;");
        //            var table = DataAccess.Instance.ExecuteSelect(cmd);
        //            return ConvertToList(table);
        //        }

        //        public List<Vin> FindBySelection(string criteres)
        //        {
        //            var cmd = new NpgsqlCommand("SELECT * FROM VIN WHERE nomvin ILIKE @crit;");
        //            cmd.Parameters.AddWithValue("@crit", "%" + criteres + "%");
        //            var table = DataAccess.Instance.ExecuteSelect(cmd);
        //            return ConvertToList(table);
        //        }

        //        private List<Vin> ConvertToList(DataTable table)
        //        {
        //            var list = new List<Vin>();

        //            foreach (DataRow row in table.Rows)
        //            {
        //                list.Add(new Vin
        //                {
        //                    NumVin = Convert.ToInt32(row["numvin"]),
        //                    NomVin = row["nomvin"].ToString(),
        //                    PrixVin = row["prixvin"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["prixvin"]),
        //                    Descriptif = row["descriptif"].ToString(),
        //                    Millesime = row["millesime"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["millesime"]),
        //                    NumType = Convert.ToInt32(row["numtype"]),
        //                    NumAppelation = Convert.ToInt32(row["numappelation"]),
        //                    NumFournisseur = Convert.ToInt32(row["numfournisseur"])
        //                });
        //            }

        //            return list;
        //        }
    }
}
