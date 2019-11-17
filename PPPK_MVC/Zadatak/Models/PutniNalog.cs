namespace Zadatak.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    public class PutniNalog
    { 

        [Key]
        [XmlElement("idPutniNalog")]
        public int? IDPutniNalog { get; set; }

        [Required]
        [XmlElement("vozacId")]
        public int? VozacID { get; set; }
        [XmlElement("voziloId")]
        public int? VoziloID { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [XmlElement("datumOdlaska")]
        public DateTime? DatumOdlaska { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [XmlElement("datumDolaska")]
        public DateTime? DatumDolaska { get; set; }

        [XmlElement("brojSati")]
        public int? BrojSati { get; set; }
        [XmlElement("brojDnevnica")]
        public int? BrojDnevnica { get; set; }
        [XmlElement("iznosDnevnice")]
        public int? IznosDnevnice { get; set; }

        [StringLength(100)]
        [XmlElement("opis")]
        public string Opis { get; set; }

        [XmlIgnore]
        public virtual Vozac Vozac { get; set; }

        [XmlIgnore]
        public string PrikazNaloga
        {
            get
            {
                return "ID: " + IDPutniNalog + ", " + Vozac.FirstName + " " + Vozac.LastName + ", " + DatumOdlaska;
            }
        }
    }
}
