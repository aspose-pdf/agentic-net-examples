using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Export form fields to a MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                // The Form object provides ExportToJson that writes JSON directly to a stream
                doc.Form.ExportToJson(ms);

                // Reset stream position before reading its contents
                ms.Position = 0;

                // Write the JSON data from the MemoryStream to a file on disk
                using (FileStream file = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"Form fields exported to JSON file: {jsonPath}");
    }
}