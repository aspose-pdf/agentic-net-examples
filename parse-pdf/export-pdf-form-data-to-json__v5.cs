using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJsonPath = "formData.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Create a FileStream for the JSON output
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form data directly to the stream as JSON using the correct API
                doc.Form.ExportToJson(jsonStream);
            }

            Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
        }
    }
}