using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportAnnotationsToJson
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string inputPdfPath = "input.pdf";
        // Output JSON file that will hold the exported annotations
        const string outputJsonPath = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the annotation editor facade bound to the loaded document
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(pdfDoc))
                {
                    // Export all annotations to XFDF format using an in‑memory stream
                    using (MemoryStream xfdfStream = new MemoryStream())
                    {
                        annotEditor.ExportAnnotationsToXfdf(xfdfStream);

                        // Reset stream position to read the exported XFDF content
                        xfdfStream.Position = 0;
                        string xfdfXml;
                        using (StreamReader reader = new StreamReader(xfdfStream))
                        {
                            xfdfXml = reader.ReadToEnd();
                        }

                        // Wrap the XFDF XML string in a simple JSON object
                        var jsonObject = new { xfdf = xfdfXml };
                        string jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                        // Write the JSON string to the output file
                        File.WriteAllText(outputJsonPath, jsonString);
                    }
                }
            }

            Console.WriteLine($"Annotations exported to JSON file: {outputJsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}