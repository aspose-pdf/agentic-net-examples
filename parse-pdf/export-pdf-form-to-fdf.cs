using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "output.json"; // Exported as JSON since core API lacks ExportFdf

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the document has a form with fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Create a FileStream for the JSON output (core API provides ExportToJson)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form data successfully exported to '{jsonPath}'.");
    }
}
