using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class PutniNalogRepository : IRepository<PutniNalog>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;


        public int Add(PutniNalog putniNalog)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddPutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", putniNalog.IDPutniNalog.HasValue ? putniNalog.IDPutniNalog.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VozacID", putniNalog.VozacID);
                    cmd.Parameters.AddWithValue("@DatumOdlaska", putniNalog.DatumOdlaska);
                    cmd.Parameters.AddWithValue("@DatumDolaska", putniNalog.DatumDolaska);
                    cmd.Parameters.AddWithValue("@BrojSati", putniNalog.BrojSati);
                    cmd.Parameters.AddWithValue("@BrojDnevnica", putniNalog.BrojDnevnica);
                    cmd.Parameters.AddWithValue("@IznosDnevnice", putniNalog.IznosDnevnice);
                    cmd.Parameters.AddWithValue("@Opis", putniNalog.Opis);
                    cmd.Parameters.AddWithValue("@VoziloID", putniNalog.VoziloID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idPutniNalog)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeletePutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", idPutniNalog);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public PutniNalog GetById(int? idPutniNalog)
        {
            PutniNalog nalog = new PutniNalog();
            IRepository<Vozac> vozacRepo = new VozacRepository();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetPutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", idPutniNalog);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            try
                            {
                                nalog = new PutniNalog
                                {
                                    IDPutniNalog = (int)r[nameof(PutniNalog.IDPutniNalog)],
                                    VozacID = (int)r[nameof(PutniNalog.VozacID)],
                                    DatumOdlaska = DateTime.Parse(r[nameof(PutniNalog.DatumDolaska)].ToString()),
                                    DatumDolaska = DateTime.Parse(r[nameof(PutniNalog.DatumOdlaska)].ToString()),
                                    BrojSati = (int)r[nameof(PutniNalog.BrojSati)],
                                    BrojDnevnica = (int)r[nameof(PutniNalog.BrojDnevnica)],
                                    IznosDnevnice = (int)r[nameof(PutniNalog.IznosDnevnice)],
                                    Opis = r[nameof(PutniNalog.Opis)].ToString(),
                                    VoziloID = (int)r[nameof(PutniNalog.VoziloID)]
                                };
                                nalog.Vozac = vozacRepo.GetById(nalog.VozacID);
                            }
                            catch (InvalidCastException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                }
            }
            return nalog;
        }

        public IEnumerable<PutniNalog> List()
        {
            IList<PutniNalog> list = new List<PutniNalog>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetPutniNalozi";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new PutniNalog
                                {
                                    IDPutniNalog = (int)r[nameof(PutniNalog.IDPutniNalog)],
                                    VozacID = (int)r[nameof(PutniNalog.VozacID)],
                                    DatumOdlaska = DateTime.Parse(r[nameof(PutniNalog.DatumOdlaska)].ToString()),
                                    DatumDolaska = DateTime.Parse(r[nameof(PutniNalog.DatumDolaska)].ToString()),
                                    BrojSati = (int)r[nameof(PutniNalog.BrojSati)],
                                    BrojDnevnica = (int)r[nameof(PutniNalog.BrojDnevnica)],
                                    IznosDnevnice = (int)r[nameof(PutniNalog.IznosDnevnice)],
                                    Opis = r[nameof(PutniNalog.Opis)].ToString(),
                                    VoziloID = (int)r[nameof(PutniNalog.VoziloID)]
                                });
                            }
                            catch (InvalidCastException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                    }
                    return list;
                }
            }

        }

        public int Update(PutniNalog putniNalog)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdatePutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", putniNalog.IDPutniNalog);
                    cmd.Parameters.AddWithValue("@VozacID", putniNalog.VozacID);
                    cmd.Parameters.AddWithValue("@DatumOdlaska", putniNalog.DatumOdlaska.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@DatumDolaska", putniNalog.DatumDolaska.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@BrojSati", putniNalog.BrojSati);
                    cmd.Parameters.AddWithValue("@BrojDnevnica", putniNalog.BrojDnevnica);
                    cmd.Parameters.AddWithValue("@IznosDnevnice", putniNalog.IznosDnevnice);
                    cmd.Parameters.AddWithValue("@Opis", putniNalog.Opis);
                    cmd.Parameters.AddWithValue("@VoziloID", putniNalog.VoziloID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}