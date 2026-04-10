using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the PDF form template, JSON data and the output PDF
        const string templatePdfPath = "template.pdf";
        const string jsonDataPath    = "data.json";
        const string outputPdfPath   = "filled.pdf";

        // Verify that the required files exist
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

        // Load the PDF form using the Form facade and import JSON values
        using (Form form = new Form(templatePdfPath))
        {
            // Import JSON stream – keys must match the full field names in the PDF
            using (FileStream jsonStream = File.OpenRead(jsonDataPath))
            {
                form.ImportJson(jsonStream);
            }

            // Save the PDF with fields populated from the JSON
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with pre‑filled form fields saved to '{outputPdfPath}'.");
    }
}