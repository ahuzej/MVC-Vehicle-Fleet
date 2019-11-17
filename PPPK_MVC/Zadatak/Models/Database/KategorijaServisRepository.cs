using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class KategorijaServisRepository : IRepository<KategorijaServis>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(KategorijaServis kategorija)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddKategorijaServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaServis", kategorija.IDKategorijaServis.HasValue ? kategorija.IDKategorijaServis.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Naziv", kategorija.Naziv);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idKategorijaServis)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteKategorijaServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaServis", idKategorijaServis);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public KategorijaServis GetById(int? idKategorijaServis)
        {
            KategorijaServis kategorijaServis = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetKategorijaServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDKategorijaServis", idKategorijaServis);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            kategorijaServis = new KategorijaServis
                            {
                                IDKategorijaServis = (int)r[nameof(KategorijaServis.IDKategorijaServis)],
                                Naziv = r[nameof(KategorijaServis.Naziv)].ToString()
                            };

                        }
                    }
                }
            }
            return kategorijaServis;
        }

        public IEnumerable<KategorijaServis> List()
        {
            IList<KategorijaServis> list = new List<KategorijaServis>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetKategorijeServisa";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new KategorijaServis
                            {
                                IDKategorijaServis = (int)r[nameof(KategorijaServis.IDKategorijaServis)],
                                Naziv = r[nameof(KategorijaServis.Naziv)].ToString()

                            });

                        }
                    }
                    return list;
                }
            }
        }
    }
}