namespace Zadatak.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    public class Drzava
    {
        [Key]
        [XmlElement("idDrzava")]
        public int? IDDrzava { get; set; }

        [StringLength(30)]
        [XmlElement("naziv")]
        public string Naziv { get; set; }

    }
}
