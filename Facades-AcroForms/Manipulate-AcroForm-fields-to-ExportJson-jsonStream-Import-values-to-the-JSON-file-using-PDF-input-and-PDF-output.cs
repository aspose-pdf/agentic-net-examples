using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF with AcroForm
        const string outputPdfPath = "output.pdf";         // PDF after importing JSON values
        const string jsonPath      = "formData.json";      // temporary JSON file

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Export form fields to JSON ----------
        // Form facade implements IDisposable, so wrap it in a using block
        using (Form exportForm = new Form(inputPdfPath))
        {
            // Create (or overwrite) the JSON file
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to the JSON stream (indented output by default)
                exportForm.ExportJson(jsonStream);
                // jsonStream will be closed automatically by the using block
            }
        }

        // ---------- Import JSON values into a new PDF ----------
        // Load the same (or another) PDF document via the Form facade
        using (Form importForm = new Form(inputPdfPath))
        {
            // Open the previously created JSON file for reading
            using (FileStream jsonReadStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values from the JSON stream into the PDF form
                importForm.ImportJson(jsonReadStream);
                // jsonReadStream will be closed automatically
            }

            // Save the modified PDF to the desired output location
            importForm.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields exported to JSON ({jsonPath}) and imported into PDF ({outputPdfPath}).");
    }
}