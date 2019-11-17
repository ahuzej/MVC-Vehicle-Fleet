using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class DrzavaRepository : IRepository<Drzava>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(Drzava drzava)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddDrzava";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDDrzava", drzava.IDDrzava.HasValue ? drzava.IDDrzava.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Naziv", drzava.Naziv);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idDrzava)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteDrzava";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDDrzava", idDrzava);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public Drzava GetById(int? idDrzava)
        {
            Drzava drzava = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetGrad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDDrzava", idDrzava);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            drzava = new Drzava
                            {
                                IDDrzava = (int)r[nameof(Drzava.IDDrzava)],
                                Naziv = r[nameof(Drzava.Naziv)].ToString()
                            };

                        }
                    }
                }
            }
            return drzava;
        }

        public IEnumerable<Drzava> List()
        {
            IList<Drzava> list = new List<Drzava>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetDrzave";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Drzava
                            {
                                IDDrzava = (int)r[nameof(Drzava.IDDrzava)],
                                Naziv = r[nameof(Drzava.Naziv)].ToString()
                            });
                        }
                    }
                    return list;
                }
            }
        }
    }
}