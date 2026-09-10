using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string outputPdfPath = "filled.pdf";  // destination PDF after filling
        const string jsonPath      = "data.json";   // JSON file: { "FieldName1":"Value1", "FieldName2":"Value2", ... }

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – {jsonPath}");
            return;
        }

        // Form facade works with two file names: source and destination.
        // It implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the JSON file as a stream and import all field values.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);   // matches fields by their full names
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form fields have been populated and saved to '{outputPdfPath}'.");
    }
}