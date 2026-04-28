using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // PDF with form fields
        const string jsonDataPath    = "data.json";      // JSON where keys match field names
        const string outputPdfPath   = "filled_form.pdf";

        // Ensure input files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load the PDF form using the Facades Form class and import JSON data
        using (Form form = new Form(templatePdfPath))
        {
            // Import field values from JSON (keys must match full field names)
            using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            // Save the filled PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPdfPath}'.");
    }
}