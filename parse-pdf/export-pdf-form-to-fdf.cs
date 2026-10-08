using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "output.json"; // Exported as JSON since core API does not support FDF

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (core API)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Check whether the document contains any form fields
                if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
                {
                    Console.WriteLine("The PDF does not contain any form fields.");
                }
                else
                {
                    // Create a FileStream for the JSON output (core API supports ExportToJson)
                    using (FileStream jsonStream = new FileStream(
                        outputJsonPath,
                        FileMode.Create,
                        FileAccess.Write))
                    {
                        // Export form data to JSON using the core Form API
                        pdfDoc.Form.ExportToJson(jsonStream);
                    }

                    Console.WriteLine($"Form data successfully exported to: {outputJsonPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
