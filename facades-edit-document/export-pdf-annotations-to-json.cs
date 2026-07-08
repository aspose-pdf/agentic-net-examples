using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor and bind the PDF document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Export all annotations to XFDF using a memory stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0;

                    // Read the XFDF XML content as a string
                    string xfdfXml;
                    using (StreamReader reader = new StreamReader(xfdfStream))
                    {
                        xfdfXml = reader.ReadToEnd();
                    }

                    // Wrap the XFDF XML in a simple JSON structure
                    var jsonWrapper = new
                    {
                        xfdf = xfdfXml
                    };

                    // Serialize to formatted JSON
                    string json = JsonSerializer.Serialize(
                        jsonWrapper,
                        new JsonSerializerOptions { WriteIndented = true });

                    // Write the JSON to the output file
                    File.WriteAllText(outputJsonPath, json);
                }
            }

            Console.WriteLine($"Annotations exported to JSON: '{outputJsonPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}