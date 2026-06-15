using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file with indented formatting
        const string outputJsonPath = "formData.json";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure export options: enable indentation for readability
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export all form fields to JSON using a file stream
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.Form.ExportToJson(jsonStream, jsonOptions);
            }

            Console.WriteLine($"Form data exported to '{outputJsonPath}' with indentation.");
        }
    }
}