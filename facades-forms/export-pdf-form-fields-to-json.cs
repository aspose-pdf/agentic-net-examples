using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(pdfPath))
        {
            // Create the output JSON file stream
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field definitions to JSON (indented for readability)
                form.ExportJson(jsonStream, true);
            }
        }

        Console.WriteLine($"Form fields exported successfully to '{jsonPath}'.");
    }
}