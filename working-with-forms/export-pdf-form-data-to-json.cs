using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // Source PDF containing form fields
        const string outputJsonPath = "formdata.json"; // Destination text file for JSON

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all form fields to JSON and write directly to the specified file.
            // This uses the Form.ExportToJson(string) overload, which handles the stream internally.
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"Form data successfully exported to '{outputJsonPath}'.");
    }
}