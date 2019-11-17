using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class GradRepository : IRepository<Grad>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(Grad grad)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddGrad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDGrad", grad.IDGrad.HasValue ? grad.IDGrad.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DrzavaID", grad.DrzavaID);
                    cmd.Parameters.AddWithValue("@Naziv", grad.Naziv);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idGrad)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteGrad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", idGrad);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public Grad GetById(int? gradId)
        {
            Grad grad = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetGrad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDGrad", gradId);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            grad = new Grad
                            {
                                IDGrad = (int)r[nameof(Grad.IDGrad)],
                                DrzavaID = (int)r[nameof(Grad.DrzavaID)],
                                Naziv = r[nameof(Grad.Naziv)].ToString()
                            };

                        }
                    }
                }
            }
            return grad;
        }

        public IList<Grad> GetByDrzava(int? idDrzava)
        {
            IList<Grad> list = new List<Grad>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetGradoviZaDrzavu";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDDrzava", idDrzava);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Grad
                            {
                                IDGrad = (int)r[nameof(Grad.IDGrad)],
                                Naziv = r[nameof(Grad.Naziv)].ToString(),
                                DrzavaID = (int)r[nameof(Grad.DrzavaID)],
                            });
                        }
                    }
                    return list;
                }
            }
        }

        public IEnumerable<Grad> List()
        {
            IList<Grad> list = new List<Grad>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetGradovi";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Grad
                            {
                                IDGrad = (int)r[nameof(Grad.IDGrad)],
                                Naziv = r[nameof(Grad.Naziv)].ToString(),
                                DrzavaID = (int)r[nameof(Grad.DrzavaID)],
                            });
                        }
                    }
                    return list;
                }
            }
        }
    }
}