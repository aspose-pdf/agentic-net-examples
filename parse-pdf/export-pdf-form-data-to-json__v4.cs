using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "formData.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document; using ensures proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream to write the JSON output.
            using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to JSON directly into the stream.
                // No ExportFieldsToJsonOptions are required for default behavior.
                doc.Form.ExportToJson(fs);
            }
        }

        Console.WriteLine($"Form data exported to '{jsonPath}'.");
    }
}