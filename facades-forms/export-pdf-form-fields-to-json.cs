using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing the form
        const string pdfPath = "input.pdf";

        // Path where the exported JSON will be saved
        const string jsonPath = "form_fields.json";

        // Ensure the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Initialize the Form facade with the PDF document
        using (Form form = new Form(pdfPath))
        {
            // Create a file stream for the JSON output (overwrite if exists)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field definitions to JSON.
                // The second parameter 'true' enables indented (pretty‑printed) output.
                form.ExportJson(jsonStream, true);
            }
        }

        Console.WriteLine($"Form fields exported to JSON at '{jsonPath}'.");
    }
}