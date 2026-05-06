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
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF document
        using (Form form = new Form(pdfPath))
        {
            // Export all form field definitions to a JSON file (indented for readability)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, true);
            }
        }

        Console.WriteLine($"Form field definitions exported to '{jsonPath}'.");
    }
}