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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and bind it to the annotation editor.
        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfDoc);

            // Export all annotations to an XFDF stream (XML format).
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset for reading.

                // Read the XFDF content as a UTF‑8 string.
                string xfdfContent = Encoding.UTF8.GetString(xfdfStream.ToArray());

                // Wrap the XFDF XML inside a simple JSON object.
                string json = JsonSerializer.Serialize(new { xfdf = xfdfContent }, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file.
                File.WriteAllText(outputJsonPath, json, Encoding.UTF8);
            }

            // Save the (unchanged) PDF if needed; here we only export annotations.
        }

        Console.WriteLine($"Annotations exported to JSON file: {outputJsonPath}");
    }
}