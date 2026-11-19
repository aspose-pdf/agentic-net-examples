using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML data into a new PDF document
        using (Document doc = new Document())
        {
            // Bind the XML source; this creates PDF pages based on the XML content
            doc.BindXml(xmlPath);

            // Set PDF version to 1.7 using Document.Convert (Version property is read‑only)
            using (var conversionLog = new MemoryStream())
            {
                doc.Convert(conversionLog, PdfFormat.v_1_7, ConvertErrorAction.Delete);
            }

            // Save the generated PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with version 1.7: {pdfPath}");
    }
}
