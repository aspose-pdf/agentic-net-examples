using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "metadata.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            xmpMetadata.BindPdf(inputPath);
            byte[] xmlBytes = xmpMetadata.GetXmpMetadata();
            string xmlString = Encoding.UTF8.GetString(xmlBytes);

            XDocument xDocument = XDocument.Parse(xmlString);
            string jsonString = JsonSerializer.Serialize(xDocument, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(outputPath, jsonString);
            Console.WriteLine($"XMP metadata exported to '{outputPath}'.");
        }
    }
}