using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // PDF with form fields
        const string outputPdf = "filled.pdf";     // Resulting PDF after import
        const string jsonPath  = "data.json";      // JSON containing field values

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Create the Form facade, specifying input and output PDFs
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Open the JSON stream and import the data.
            // Missing fields in the PDF are ignored automatically.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdf}'.");
    }
}