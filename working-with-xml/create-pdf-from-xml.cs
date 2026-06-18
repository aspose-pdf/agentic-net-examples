using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Sample XML content to be converted to PDF
        string xmlContent = "<root><message>Hello World</message></root>";
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
        using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
        {
            using (Document pdfDocument = new Document())
            {
                // Load the XML from the memory stream into the PDF document
                pdfDocument.BindXml(xmlStream);
                // Save the resulting PDF with default settings
                pdfDocument.Save("output.pdf");
            }
        }
    }
}