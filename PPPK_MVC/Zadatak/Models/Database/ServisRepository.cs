using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models.Database
{
    public class ServisRepository : IRepository<Servis>
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        IRepository<KategorijaServis> kategorijaServisRepository = new KategorijaServisRepository();

        public int Add(Servis servis)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDServis", servis.IDServis.HasValue ? servis.IDServis.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VoziloID", servis.VoziloID);
                    cmd.Parameters.AddWithValue("@Cijena", servis.Cijena);
                    cmd.Parameters.AddWithValue("@Opis", servis.Opis);
                    cmd.Parameters.AddWithValue("@Datum", servis.Datum.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@KategorijaServisID", servis.KategorijaServisID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public int Delete(int? idServis)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDServis", idServis);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Servis GetById(int? idServis)
        {
            Servis servis = null;
            IRepository<Vozilo> voziloRepository = new VoziloRepository();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDServis", idServis);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            servis = new Servis
                            {
                                IDServis = (int)r[nameof(Servis.IDServis)],
                                VoziloID = (int)r[nameof(Servis.VoziloID)],
                                Cijena = (double)r[nameof(Servis.Cijena)],
                                Opis = r[nameof(Servis.Opis)].ToString(),
                                Datum = DateTime.Parse(r[nameof(Servis.Datum)].ToString()),
                                KategorijaServisID = (int)r[nameof(Servis.KategorijaServisID)],
                            };
                            servis.Vozilo = voziloRepository.GetById(servis.VoziloID);
                            servis.KategorijaServis = kategorijaServisRepository.GetById(servis.KategorijaServisID);

                        }
                    }
                }
            }
            return servis;
        }

        public IEnumerable<Servis> List()
        {
            IRepository<Vozilo> voziloRepository = new VoziloRepository();
            IList<Servis> list = new List<Servis>();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "GetServisi";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                list.Add(new Servis
                                {
                                    IDServis = (int)r[nameof(Servis.IDServis)],
                                    VoziloID = (int)r[nameof(Servis.VoziloID)],
                                    Cijena = (double)r[nameof(Servis.Cijena)],
                                    Opis = r[nameof(Servis.Opis)].ToString(),
                                    Datum = DateTime.Parse(r[nameof(Servis.Datum)].ToString()),
                                    KategorijaServisID = (int)r[nameof(Servis.KategorijaServisID)],
                                    Vozilo = voziloRepository.GetById((int)r[nameof(Servis.VoziloID)]),
                                    KategorijaServis = kategorijaServisRepository.GetById((int)r[nameof(Servis.KategorijaServisID)])
                                });

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return list;
        }

        public int Update(Servis servis)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateServis";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDServis", servis.IDServis);
                    cmd.Parameters.AddWithValue("@VoziloID", servis.VoziloID);
                    cmd.Parameters.AddWithValue("@Cijena", servis.Cijena);
                    cmd.Parameters.AddWithValue("@Opis", servis.Opis);
                    cmd.Parameters.AddWithValue("@Datum", servis.Datum.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@KategorijaServisID", servis.KategorijaServisID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}