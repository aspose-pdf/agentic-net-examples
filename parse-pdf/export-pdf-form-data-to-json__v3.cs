using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream that will receive the JSON output
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields directly to the stream as JSON
                // No options are required; default behavior is used
                pdfDoc.Form.ExportToJson(jsonStream);
            }

            // Alternative overload (commented) – export directly to a file path
            // pdfDoc.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"Form data exported to '{outputJsonPath}'.");
    }
}