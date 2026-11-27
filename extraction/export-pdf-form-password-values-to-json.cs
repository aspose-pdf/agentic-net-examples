using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure export options: only export password values of form fields
            ExportFieldsToJsonOptions exportOptions = new ExportFieldsToJsonOptions
            {
                ExportPasswordValue = true
                // All other options keep their default values
            };

            // Export form fields to JSON using the overload that takes a Stream first, then options
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.Form.ExportToJson(jsonStream, exportOptions);
            }
        }

        Console.WriteLine($"Form fields exported to JSON: {outputJsonPath}");
    }
}
