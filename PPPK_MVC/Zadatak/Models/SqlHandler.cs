using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class SqlHandler
    {
        private const string CMD_GETRELACIJE = "SELECT * FROM Relacija";
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        internal static IList<Vozac> GetVozaci()
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

        internal static IList<KategorijaTroska> GetKategorijeTroskova()
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
                            list.Add(new KategorijaTroska {
                            IDKategorijaTrosak = (int)r[nameof(KategorijaTroska.IDKategorijaTrosak)],
                            Naziv = r[nameof(KategorijaTroska.Naziv)].ToString()
                            });
                        }
                    }
                    return list;
                }
            }
        }

        internal static PutniNalog GetPutniNalog(int? putniNalogID)
        {
            PutniNalog nalog = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetPutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", putniNalogID);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
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
                            nalog.Vozac = GetVozac(nalog.VozacID);
                        }
                    }
                }
            }
            return nalog;
        }

        internal static Grad GetGrad(int? gradId)
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

        internal static IList<Relacija> GetRelacije()
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
                    }
                    return list;
                }
            }
        }

        internal static void AddDrzava(Drzava drzava)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddDrzava";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Naziv", drzava.Naziv);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void AddKategorijaTrosak(KategorijaTroska kategorija)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddKategorijaTrosak";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Naziv", kategorija.Naziv);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void AddGrad(Grad grad)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddGrad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DrzavaID", grad.DrzavaID);
                    cmd.Parameters.AddWithValue("@Naziv", grad.Naziv);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static Relacija GetRelacija(int? relacijaId)
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

        internal static void AddVozilo(Vozilo vozilo)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tip", vozilo.Tip);
                    cmd.Parameters.AddWithValue("@Marka", vozilo.Marka);
                    cmd.Parameters.AddWithValue("@GodinaProizvodnje", vozilo.GodinaProizvodnje);
                    cmd.Parameters.AddWithValue("@StanjeKilometra", vozilo.StanjeKilometra);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static int UpdatePutniNalog(PutniNalog nalog)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdatePutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", nalog.IDPutniNalog);
                    cmd.Parameters.AddWithValue("@VozacID", nalog.VozacID);
                    cmd.Parameters.AddWithValue("@DatumOdlaska", nalog.DatumOdlaska.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@DatumDolaska", nalog.DatumDolaska.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@BrojSati", nalog.BrojSati);
                    cmd.Parameters.AddWithValue("@BrojDnevnica", nalog.BrojDnevnica);
                    cmd.Parameters.AddWithValue("@IznosDnevnice", nalog.IznosDnevnice);
                    cmd.Parameters.AddWithValue("@Opis", nalog.Opis);
                    cmd.Parameters.AddWithValue("@VoziloID", nalog.VoziloID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }

        internal static int AddRelacija(Relacija relacija)
        {
            //using (SqlConnection con = new SqlConnection(cs))
            //{
            //    con.Open();
            //    using (SqlCommand cmd = con.CreateCommand())
            //    {
            //        cmd.CommandText = "AddRelacija";
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@GradPolazakID", relacija.GradPolazakID);
            //        cmd.Parameters.AddWithValue("@GradDolazakID", relacija.GradDolazakID);
            //        cmd.Parameters.AddWithValue("@PutniNalogID", relacija.PutniNalogID);
            //        cmd.Parameters.AddWithValue("@Kilometraza", relacija.Kilometraza);
            //        cmd.Parameters.AddWithValue("@PrijevozIznos", relacija.PrijevozIznos);


            //        return cmd.ExecuteNonQuery();

            //    }
            //}
            return SqlHelper.ExecuteNonQuery(cs, "AddRelacija", relacija.GradPolazakID, relacija.GradDolazakID, relacija.PutniNalogID, relacija.Kilometraza, relacija.PrijevozIznos);
        }

        internal static int DeletePutniNalog(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeletePutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPutniNalog", id);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        internal static Vozilo GetVozilo(int? voziloID)
        {
            Vozilo vozilo = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozilo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozilo", voziloID);
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

        internal static IList<Grad> GetGradoviZaDrzavu(int drzava)
        {
            IList<Grad> list = new List<Grad>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetGradoviZaDrzavu";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDDrzava", drzava);
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

        internal static IList<Grad> GetGradovi()
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

        internal static IList<Drzava> GetDrzave()
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



        internal static IList<PutniNalog> GetPutniNalozi()
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
                    }
                    return list;
                }
            }
        }

        internal static DataTable GetTblRelacije()
        {
            SqlDataAdapter da = new SqlDataAdapter(CMD_GETRELACIJE, new SqlConnection(cs));
            DataTable tblRelacije = new DataTable("Relacije");
            da.Fill(tblRelacije);

            return tblRelacije;
        }

        internal static IList<Vozilo> GetVozila()
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

        internal static int AddPutniNalog(PutniNalog nalog)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddPutniNalog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VozacID", nalog.VozacID);
                    cmd.Parameters.AddWithValue("@DatumOdlaska", nalog.DatumOdlaska);
                    cmd.Parameters.AddWithValue("@DatumDolaska", nalog.DatumDolaska);
                    cmd.Parameters.AddWithValue("@BrojSati", nalog.BrojSati);
                    cmd.Parameters.AddWithValue("@BrojDnevnica", nalog.BrojDnevnica);
                    cmd.Parameters.AddWithValue("@IznosDnevnice", nalog.IznosDnevnice);
                    cmd.Parameters.AddWithValue("@Opis", nalog.Opis);
                    cmd.Parameters.AddWithValue("@VoziloID", nalog.VoziloID);


                    return cmd.ExecuteNonQuery();

                }
            }
        }

        internal static int AddVozac(Vozac vozac)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", vozac.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", vozac.LastName);
                    cmd.Parameters.AddWithValue("@Mobitel", vozac.Mobitel);
                    cmd.Parameters.AddWithValue("@VozackaDozvola", vozac.VozackaDozvola);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        internal static Vozac GetVozac(int? id)
        {
            Vozac vozac = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozac", id);
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
            return vozac;
        }

        internal static int DeleteVozac(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteVozac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVozac", id);

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        internal static int UpdateVozac(Vozac vozac)
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