using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class VoziloRepository : IRepository<Vozilo>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;


        public int Add(Vozilo vozilo)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.FireInfoMessageEventOnUserErrors = true;
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozilo", vozilo.IDVozilo.HasValue ? vozilo.IDVozilo.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tip", vozilo.Tip);
                    cmd.Parameters.AddWithValue("@Marka", vozilo.Marka);
                    cmd.Parameters.AddWithValue("@GodinaProizvodnje", vozilo.GodinaProizvodnje);
                    cmd.Parameters.AddWithValue("@StanjeKilometra", vozilo.StanjeKilometra);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        private void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string errors = "";
            foreach (SqlError err in e.Errors)
            {
                errors += err.LineNumber.ToString() + ": " + err.Message + "\r\n";
            }
            Console.WriteLine(errors);
        }

        public int Delete(int? idVozilo)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozilo", idVozilo);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public Vozilo GetById(int? idVozilo)
        {
            Vozilo vozilo = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozilo", idVozilo);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            vozilo = new Vozilo
                            {
                                IDVozilo = (int)r[nameof(Vozilo.IDVozilo)],
                                Tip = r[nameof(Vozilo.Tip)].ToString(),
                                Marka = r[nameof(Vozilo.Marka)].ToString(),
                                GodinaProizvodnje = (int)r[nameof(Vozilo.GodinaProizvodnje)],
                                StanjeKilometra = (int)r[nameof(Vozilo.StanjeKilometra)]
                            };

                        }
                    }
                }
            }
            return vozilo;
        }

        public IEnumerable<Vozilo> List()
        {
            IList<Vozilo> list = new List<Vozilo>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozila";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Vozilo
                            {
                                IDVozilo = (int)r[nameof(Vozilo.IDVozilo)],
                                Tip = r[nameof(Vozilo.Tip)].ToString(),
                                Marka = r[nameof(Vozilo.Marka)].ToString(),
                                GodinaProizvodnje = (int)r[nameof(Vozilo.GodinaProizvodnje)],
                                StanjeKilometra = (int)r[nameof(Vozilo.StanjeKilometra)]
                            });
                        }
                    }
                    return list;
                }
            }
        }

        public int Update(Vozilo vozilo)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozilo", vozilo.IDVozilo);
                    cmd.Parameters.AddWithValue("@Tip", vozilo.Tip);
                    cmd.Parameters.AddWithValue("@Marka", vozilo.Marka);
                    cmd.Parameters.AddWithValue("@GodinaProizvodnje", vozilo.GodinaProizvodnje.Value);
                    cmd.Parameters.AddWithValue("@StanjeKilometra", vozilo.StanjeKilometra);

                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}