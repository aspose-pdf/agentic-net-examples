using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF with form fields
        const string outputJsonPath = "output.json"; // destination JSON file (used instead of FDF due to API constraints)

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Open a FileStream for writing the form data as JSON
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form data to the JSON stream (core API alternative to ExportFdf)
                doc.Form.ExportToJson(jsonStream);
                // The using statement will automatically flush and close the stream
            }
        }

        Console.WriteLine($"Form data exported to JSON: {outputJsonPath}");
    }
}
