using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class RelacijaRepository : IRepository<Relacija>
    {
        private const string CMD_GETRELACIJE = "SELECT * FROM Relacija";
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(Relacija relacija)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(cs, "AddRelacija",
                    relacija.IDRelacija.HasValue ? relacija.IDRelacija.Value : (object)DBNull.Value,
                    relacija.GradPolazakID,
                    relacija.GradDolazakID,
                    relacija.PutniNalogID,
                    relacija.Kilometraza,
                    relacija.PrijevozIznos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }

        public int Delete(int? idRelacija)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteRelacija";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDRelacija", idRelacija);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Relacija GetById(int? relacijaId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "GetRelacija", relacijaId);
            Relacija r = new Relacija();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                r.IDRelacija = relacijaId.Value;
                r.GradDolazakID = int.Parse(row["GradDolazakID"].ToString());
                r.GradPolazakID = int.Parse(row["GradPolazakID"].ToString());
                r.PutniNalogID = int.Parse(row["PutniNalogID"].ToString());
                r.Kilometraza = int.Parse(row["Kilometraza"].ToString());
                r.PrijevozIznos = int.Parse(row["PrijevozIznos"].ToString());
            }
            return r;
        }

        public DataTable GetTblRelacije()
        {
            SqlDataAdapter da = new SqlDataAdapter(CMD_GETRELACIJE, new SqlConnection(cs));
            DataTable tblRelacije = new DataTable("Relacije");
            da.Fill(tblRelacije);

            return tblRelacije;
        }

        public IEnumerable<Relacija> List()
        {
            IList<Relacija> list = new List<Relacija>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetRelacije";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Relacija
                                {
                                    IDRelacija = (int)r[nameof(Relacija.IDRelacija)],
                                    GradPolazakID = (int)r[nameof(Relacija.GradPolazakID)],
                                    GradDolazakID = (int)r[nameof(Relacija.GradDolazakID)],
                                    PutniNalogID = (int)r[nameof(Relacija.PutniNalogID)],
                                    Kilometraza = (int)r[nameof(Relacija.Kilometraza)],
                                    PrijevozIznos = (int)r[nameof(Relacija.PrijevozIznos)],
                                });
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                    return list;
                }
            }
        }
    }
}