using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "annotations.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor and bind the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Export all annotations to XFDF using a memory stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0; // Reset stream for reading

                    // Load the XFDF XML from the stream
                    XDocument xfdfXml = XDocument.Load(xfdfStream);
                    string xmlContent = xfdfXml.ToString();

                    // Wrap the XML string in a simple JSON structure
                    string json = JsonSerializer.Serialize(
                        new { xfdf = xmlContent },
                        new JsonSerializerOptions { WriteIndented = true });

                    // Write the JSON output to the target file
                    File.WriteAllText(outputJson, json);
                }
            }
        }

        Console.WriteLine($"Annotations exported to JSON file: {outputJson}");
    }
}