using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Form class resides here

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the exported JSON file
        const string inputPdfPath  = "input_form.pdf";
        const string outputJsonPath = "form_fields.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Create a Form facade bound to the PDF file.
        // The constructor loads the PDF and prepares the form fields for access.
        using (Form form = new Form(inputPdfPath))
        {
            // Export all form fields to JSON.
            // The second parameter (includeEmptyFields) is set to false to omit empty entries.
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, false);
            }

            // Optionally, also export to FDF format for compatibility with legacy tools.
            // Uncomment the following block if FDF output is required.
            /*
            using (FileStream fdfStream = new FileStream("form_fields.fdf", FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
            */
        }

        Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
    }
}