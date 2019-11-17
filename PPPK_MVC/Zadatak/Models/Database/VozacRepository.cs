using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class VozacRepository : IRepository<Vozac>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int Add(Vozac vozac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "AddVozac";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDVozac", vozac.IDVozac.HasValue ? vozac.IDVozac.Value : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FirstName", vozac.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", vozac.LastName);
                        cmd.Parameters.AddWithValue("@Mobitel", vozac.Mobitel);
                        cmd.Parameters.AddWithValue("@VozackaDozvola", vozac.VozackaDozvola);

                        return cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (NullReferenceException ex)
            {
                return -1;
            }
        }

        public int Delete(int? idVozac)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozac", idVozac);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public Vozac GetById(int? idVozac)
        {
            Vozac vozac = new Vozac();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(idVozac.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@IDVozac", idVozac);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                vozac = new Vozac
                                {
                                    IDVozac = (int)r[nameof(Vozac.IDVozac)],
                                    FirstName = r[nameof(Vozac.FirstName)].ToString(),
                                    LastName = r[nameof(Vozac.LastName)].ToString(),
                                    Mobitel = r[nameof(Vozac.Mobitel)].ToString(),
                                    VozackaDozvola = r[nameof(Vozac.VozackaDozvola)].ToString()
                                };

                            }
                        }
                    }

                }
            }
            return vozac;
        }

        internal int UpdateVozac(Vozac vozac)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozac", vozac.IDVozac);
                    cmd.Parameters.AddWithValue("@FirstName", vozac.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", vozac.LastName);
                    cmd.Parameters.AddWithValue("@Mobitel", vozac.Mobitel);
                    cmd.Parameters.AddWithValue("@VozackaDozvola", vozac.VozackaDozvola);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public IEnumerable<Vozac> List()
        {
            IList<Vozac> list = new List<Vozac>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozaci";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Vozac
                            {
                                IDVozac = (int)r[nameof(Vozac.IDVozac)],
                                FirstName = r[nameof(Vozac.FirstName)].ToString(),
                                LastName = r[nameof(Vozac.LastName)].ToString(),
                                Mobitel = r[nameof(Vozac.Mobitel)].ToString(),
                                VozackaDozvola = r[nameof(Vozac.VozackaDozvola)].ToString()
                            });
                        }
                    }
                    return list;
                }
            }
        }

        public int Update(Vozac vozac)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozac", vozac.IDVozac);
                    cmd.Parameters.AddWithValue("@FirstName", vozac.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", vozac.LastName);
                    cmd.Parameters.AddWithValue("@Mobitel", vozac.Mobitel);
                    cmd.Parameters.AddWithValue("@VozackaDozvola", vozac.VozackaDozvola);

                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}