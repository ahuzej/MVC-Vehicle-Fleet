using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class KategorijaTrosakRepository : IRepository<KategorijaTroska>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(KategorijaTroska kategorija)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddKategorijaTrosak";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaTrosak", kategorija.IDKategorijaTrosak.HasValue ? kategorija.IDKategorijaTrosak.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Naziv", kategorija.Naziv);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idKategorijaTrosak)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteKategorijaTrosak";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaTrosak", idKategorijaTrosak);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public KategorijaTroska GetById(int? idKategorijaTrosak)
        {
            KategorijaTroska kategorijaTroska = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetKategorijaTroska";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaTrosak", idKategorijaTrosak);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            kategorijaTroska = new KategorijaTroska
                            {
                                IDKategorijaTrosak = (int)r[nameof(KategorijaTroska.IDKategorijaTrosak)],
                                Naziv = r[nameof(KategorijaTroska.Naziv)].ToString()
                            };

                        }
                    }
                }
            }
            return kategorijaTroska;
        }

        public IEnumerable<KategorijaTroska> List()
        {
            IList<KategorijaTroska> list = new List<KategorijaTroska>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetKategorijeTroskova";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new KategorijaTroska
                            {
                                IDKategorijaTrosak = (int)r[nameof(KategorijaTroska.IDKategorijaTrosak)],
                                Naziv = r[nameof(KategorijaTroska.Naziv)].ToString()
                            });
                        }
                    }
                    return list;
                }
            }
        }
    }
}