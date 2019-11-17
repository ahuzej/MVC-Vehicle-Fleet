using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Zadatak.Models
{
    [XmlRoot("pppkDb")]
    public class BackupModel
    {
        [XmlArray("listaDrzava"), XmlArrayItem(typeof(Drzava), ElementName = "drzava")]
        public List<Drzava> Drzave { get; set; }

        [XmlArray("listaGradova"), XmlArrayItem(typeof(Grad), ElementName = "grad")]
        public List<Grad> Gradovi { get; set; }

        [XmlArray("listaPutnihNaloga"), XmlArrayItem(typeof(PutniNalog), ElementName = "putniNalog")]
        public List<PutniNalog> PutniNalozi { get; set; }

        [XmlArray("listaRelacija"), XmlArrayItem(typeof(Relacija), ElementName = "relacija")]
        public List<Relacija> Relacije { get; set; }

        [XmlArray("listaTrosaka"), XmlArrayItem(typeof(Trosak), ElementName = "trosak")]
        public List<Trosak> Troskovi { get; set; }

        [XmlArray("listaVozaca"), XmlArrayItem(typeof(Vozac), ElementName = "vozac")]
        public List<Vozac> Vozaci { get; set; }

        [XmlArray("listaVozila"), XmlArrayItem(typeof(Vozilo), ElementName = "vozilo")]
        public List<Vozilo> Vozila { get; set; }

        [XmlArray("listaKategorijeTroskova"), XmlArrayItem(typeof(KategorijaTroska), ElementName = "trosak")]
        public List<KategorijaTroska> KategorijeTroskova { get; set; }

        [XmlArray("listaServisa"), XmlArrayItem(typeof(Servis), ElementName = "servis")]
        public List<Servis> Servisi { get; set; }

        [XmlArray("listaKategorijeServisa"), XmlArrayItem(typeof(KategorijaServis), ElementName = "kategorijeServisa")]
        public List<KategorijaServis> KategorijeServisa { get; set; }


        public void Save(string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();

            }
        }
    }
}