using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (with AcroForm) and the resulting PDF
        const string inputPdfPath  = "input_form.pdf";
        const string outputPdfPath = "output_form.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Export form field data from the source PDF to a JSON stream
        // -----------------------------------------------------------------
        using (Form exportForm = new Form(inputPdfPath))
        using (MemoryStream jsonStream = new MemoryStream())
        {
            // Export all form fields to JSON.
            // The second parameter (false) indicates that button field values are not exported.
            exportForm.ExportJson(jsonStream, false);

            // Reset the stream position so it can be read from the beginning.
            jsonStream.Position = 0;

            // -----------------------------------------------------------------
            // 2. Prepare the output PDF (a copy of the original) for import
            // -----------------------------------------------------------------
            // Copy the original PDF to the output location.
            // This ensures we have a writable file to import data into.
            File.Copy(inputPdfPath, outputPdfPath, true);

            // -----------------------------------------------------------------
            // 3. Import the previously exported JSON data into the new PDF
            // -----------------------------------------------------------------
            using (Form importForm = new Form(outputPdfPath))
            {
                // Import the JSON data back into the form fields.
                importForm.ImportJson(jsonStream);

                // Save changes to the same file (outputPdfPath).
                importForm.Save();
            }
        }

        Console.WriteLine($"Form data exported to JSON and re‑imported successfully. Output saved to '{outputPdfPath}'.");
    }
}