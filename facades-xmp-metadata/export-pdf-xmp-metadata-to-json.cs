using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "metadata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // The Metadata property returns a dictionary of XMP key/value pairs.
                // It replaces the obsolete PdfFileInfo.XmpMetadata member.
                var xmpMetadata = pdfDoc.Metadata; // IDictionary<string,string>

                // Serialize the metadata dictionary to formatted JSON.
                string json = JsonSerializer.Serialize(
                    xmpMetadata,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file.
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"XMP metadata exported to JSON: {outputJsonPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}