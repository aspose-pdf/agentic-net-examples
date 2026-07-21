using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Path where the extracted JSON will be saved
        const string outputJsonPath = "formFields.json";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document. The using block ensures deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all form fields to a JSON file.
            // The ExportToJson method writes the JSON representation of the form fields.
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        // Optional: read the generated JSON into a string for further processing.
        string json = File.ReadAllText(outputJsonPath);
        Console.WriteLine("Form fields extracted to JSON:");
        Console.WriteLine(json);
    }
}