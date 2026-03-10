using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Retrieve standard PDF metadata via PdfFileInfo
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            Console.WriteLine("=== Standard PDF Metadata ===");
            Console.WriteLine($"Title          : {info.Title}");
            Console.WriteLine($"Author         : {info.Author}");
            Console.WriteLine($"Subject        : {info.Subject}");
            Console.WriteLine($"Keywords       : {info.Keywords}");
            Console.WriteLine($"Creator        : {info.Creator}");
            Console.WriteLine($"Producer       : {info.Producer}");
            Console.WriteLine($"Creation Date  : {info.CreationDate}");
            Console.WriteLine($"Modification   : {info.ModDate}");
            Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
            Console.WriteLine($"Is Encrypted   : {info.IsEncrypted}");
            Console.WriteLine($"Has Open Password : {info.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password : {info.HasEditPassword}");
        }

        // Load the PDF document – required for the XMP metadata facade
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve XMP metadata via PdfXmpMetadata (expects a Document instance)
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                byte[] xmpData = xmp.GetXmpMetadata();
                string xmpXml = Encoding.UTF8.GetString(xmpData);
                Console.WriteLine("\n=== XMP Metadata (XML) ===");
                Console.WriteLine(xmpXml);
            }
        }

        // Example of retrieving a custom metadata property
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            string customValue = info.GetMetaInfo("CustomProperty");
            if (!string.IsNullOrEmpty(customValue))
                Console.WriteLine($"\nCustom Property 'CustomProperty': {customValue}");
            else
                Console.WriteLine("\nCustom Property 'CustomProperty' not found.");
        }
    }
}