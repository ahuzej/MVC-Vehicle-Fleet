using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class XmlFileWriter
    {
        private IXmlWritable writeXml { get; set; }

        public XmlFileWriter(IXmlWritable writeXml)
        {
            this.writeXml = writeXml;
        }

        public void Write(string fileName)
        {
            writeXml.Write(fileName);
        }

    }
}