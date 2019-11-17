namespace Zadatak.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    public class Grad
    {
        [Key]
        [XmlElement("idGrad")]
        public int? IDGrad { get; set; }
        [XmlElement("idDrzava")]
        public int DrzavaID { get; set; }
        [StringLength(30)]
        [XmlElement("naziv")]
        public string Naziv { get; set; }

    }
}
