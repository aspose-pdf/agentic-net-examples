using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string jsonPath      = "data.json";   // JSON file containing field values
        const string outputPdfPath = "filled_flattened.pdf";

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – {jsonPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Populate the form fields from the JSON data source
            // Form.ImportFromJson(string) reads the JSON file and assigns values to matching fields
            pdfDoc.Form.ImportFromJson(jsonPath);

            // Flatten the form so that field values become part of the page content and cannot be edited
            pdfDoc.Flatten();

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Successfully filled and flattened the PDF. Output saved to '{outputPdfPath}'.");
    }
}