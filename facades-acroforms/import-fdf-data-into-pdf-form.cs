using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "form.pdf";          // source PDF with form fields
        const string outputPdfPath = "form_filled.pdf";   // PDF after importing FDF data
        const string fdfPath       = "data.fdf";          // FDF file containing field values

        // Verify that all required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Open the FDF stream, import it into the PDF form, and save the result.
        // Form implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the FDF file as a read‑only stream.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values from the FDF stream into the PDF.
                form.ImportFdf(fdfStream);
            }

            // Persist the modified PDF to the output path.
            form.Save();
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
    }
}