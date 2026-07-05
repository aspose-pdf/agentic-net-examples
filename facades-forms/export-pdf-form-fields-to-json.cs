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
            // Create a file stream for the JSON output
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field definitions (and values) to JSON.
                // The second argument enables pretty‑printing (indented output).
                form.ExportJson(jsonStream, indented: true);
            }
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}