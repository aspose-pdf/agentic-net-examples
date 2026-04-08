using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formData.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream to write the JSON output
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to JSON; optional ExportFieldsToJsonOptions can be supplied
                pdfDoc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form data successfully exported to '{outputJsonPath}'.");
    }
}