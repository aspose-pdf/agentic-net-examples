using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access XMP metadata via the PdfXmpMetadata facade
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata(doc);

            // Retrieve the full XMP packet as XML
            byte[] xmpBytes = xmpMetadata.GetXmpMetadata();
            string xmpXml = System.Text.Encoding.UTF8.GetString(xmpBytes);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xmpXml);

            // Save the (unchanged) PDF to a new file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}