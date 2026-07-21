using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing the form
        const string pdfPath = "input.pdf";

        // Output JSON file that will receive the exported form field definitions
        const string jsonPath = "formFields.json";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Use the Facade Form class to work with AcroForm data.
        // The constructor loads the PDF document internally.
        using (Form form = new Form(pdfPath))
        {
            // Create a writable file stream for the JSON output.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to JSON.
                // The second parameter (true) requests indented (pretty‑printed) JSON.
                form.ExportJson(jsonStream, indented: true);
            }
        }

        Console.WriteLine($"Form fields exported to JSON file: {jsonPath}");
    }
}