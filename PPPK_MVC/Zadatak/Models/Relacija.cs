namespace Zadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot("relacija")]
    public class Relacija : IXmlWritable
    {
        [Key]
        [XmlElement("id", IsNullable = true)]
        public int? IDRelacija { get; set; }

        [XmlElement("gradpolazakid")]
        public int? GradPolazakID { get; set; }

        [XmlElement("graddolazakid")]
        public int? GradDolazakID { get; set; }

        [XmlElement("putninalogid")]
        public int? PutniNalogID { get; set; }

        [XmlElement("kilometraza")]
        public int? Kilometraza { get; set; }

        [XmlElement("prijevoziznos")]
        public int? PrijevozIznos { get; set; }

        public void Write(string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();

            }
        }

        public static Relacija Load(string FileName)
        {
            using (var stream = File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(typeof(Relacija));
                return serializer.Deserialize(stream) as Relacija;
            }
        }
    }
}
