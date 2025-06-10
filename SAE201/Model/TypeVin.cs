using Npgsql;
using System.Data;

namespace SAE201.Model
{
    public class TypeVin
    {
        public int NumType { get; set; }
        public string NomType { get; set; }

        public List<TypeVin> TypesVin { get; set; }

        public List<TypeVin> FindAll()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM TYPEVIN;");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        public List<TypeVin> FindBySelection(string criteres)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM TYPEVIN WHERE nomtype ILIKE @crit;");
            cmd.Parameters.AddWithValue("@crit", "%" + criteres + "%");
            var table = DataAccess.Instance.ExecuteSelect(cmd);
            return ConvertToList(table);
        }

        private List<TypeVin> ConvertToList(DataTable table)
        {
            var list = new List<TypeVin>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new TypeVin
                {
                    NumType = Convert.ToInt32(row["numtype"]),
                    NomType = row["nomtype"].ToString()
                });
            }

            return list;
        }
    }
}
