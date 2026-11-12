using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve XMP metadata via the PdfXmpMetadata facade
            byte[] xmpBytes;
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata(doc))
            {
                // GetXmpMetadata returns the raw XMP packet as a byte array
                xmpBytes = xmpFacade.GetXmpMetadata();
            }

            if (xmpBytes == null || xmpBytes.Length == 0)
            {
                Console.WriteLine("No XMP metadata found in the PDF.");
                return;
            }

            // Convert the byte[] to a UTF‑8 string
            string xmpXml = Encoding.UTF8.GetString(xmpBytes);

            // Parse the XML
            XDocument xDoc;
            try
            {
                xDoc = XDocument.Parse(xmpXml);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse XMP XML: {ex.Message}");
                return;
            }

            // Example: read Dublin Core (dc) schema properties
            XNamespace dc = "http://purl.org/dc/elements/1.1/";

            string title = xDoc.Root?
                .Descendants(dc + "title")
                .FirstOrDefault()? .Value ?? "(no title)";

            string creator = xDoc.Root?
                .Descendants(dc + "creator")
                .FirstOrDefault()? .Value ?? "(no creator)";

            Console.WriteLine($"XMP Title  : {title}");
            Console.WriteLine($"XMP Creator: {creator}");
        }
    }
}
