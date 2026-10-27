using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonOutputPath = "metadata.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Use the PdfXmpMetadata facade to obtain XMP metadata as an XML string
            using (Aspose.Pdf.Facades.PdfXmpMetadata xmpFacade = new Aspose.Pdf.Facades.PdfXmpMetadata(doc))
            {
                // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();
                string xmpXml = Encoding.UTF8.GetString(xmpBytes);

                if (string.IsNullOrEmpty(xmpXml))
                {
                    Console.WriteLine("No XMP metadata found in the document.");
                    return;
                }

                // Wrap the raw XML string into a JSON object for non‑XML systems
                // The XML is stored as a JSON string value to preserve its structure
                string json = $"{{\"XmpMetadata\": {JsonSerializer.Serialize(xmpXml)}}}";

                // Write the JSON to the output file
                File.WriteAllText(jsonOutputPath, json);
                Console.WriteLine($"XMP metadata exported to JSON file: {jsonOutputPath}");
            }
        }
    }
}
