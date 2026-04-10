using System;
using System.IO;
using System.Xml.Linq;
using System.Text.Json;
using Aspose.Pdf.Facades;

class ExportAnnotationsToJson
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string inputPdfPath = "input.pdf";
        // Output JSON file that will hold the exported annotation data
        const string outputJsonPath = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Initialize the PdfAnnotationEditor facade and bind it to the source PDF
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(inputPdfPath);

        // Export all annotations to an in‑memory XFDF (XML) stream
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            annotationEditor.ExportAnnotationsToXfdf(xfdfStream);
            // Reset stream position for reading
            xfdfStream.Position = 0;

            // Load the XFDF XML into an XDocument for easy manipulation
            XDocument xfdfXml = XDocument.Load(xfdfStream);

            // Prepare a simple JSON structure that contains the XFDF XML as a string.
            // More sophisticated conversion can be implemented if needed.
            var jsonModel = new
            {
                xfdf = xfdfXml.ToString(SaveOptions.DisableFormatting)
            };

            // Serialize the model to JSON with indentation for readability
            string jsonContent = JsonSerializer.Serialize(jsonModel, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON content to the output file
            File.WriteAllText(outputJsonPath, jsonContent);
        }

        // Clean up the facade
        annotationEditor.Close();

        Console.WriteLine($"Annotations exported to JSON file: {outputJsonPath}");
    }
}