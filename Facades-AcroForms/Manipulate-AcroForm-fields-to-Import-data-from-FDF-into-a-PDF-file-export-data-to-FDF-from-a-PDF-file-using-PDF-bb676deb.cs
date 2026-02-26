using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";          // PDF with AcroForm fields
        const string inputFdfPath   = "data.fdf";           // FDF containing field values to import
        const string outputPdfPath  = "imported.pdf";       // PDF after importing FDF data
        const string exportFdfPath  = "exported.fdf";       // FDF generated from the PDF

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {inputFdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1) Import data from FDF into the PDF
        // ------------------------------------------------------------
        // Form constructor (inputPdf, outputPdf) creates a facade bound to the source PDF
        // and prepares to save the result to outputPdf.
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the FDF file as a read‑only stream and import its field values.
            using (FileStream fdfStream = new FileStream(inputFdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the updated PDF. No path argument is needed because the output
            // file was specified in the constructor.
            form.Save();
        }

        Console.WriteLine($"Imported FDF data saved to '{outputPdfPath}'.");

        // ------------------------------------------------------------
        // 2) Export the (now possibly modified) PDF fields back to FDF
        // ------------------------------------------------------------
        using (Form exportForm = new Form(outputPdfPath))
        {
            // Create (or overwrite) the destination FDF file.
            using (FileStream outFdf = new FileStream(exportFdfPath, FileMode.Create, FileAccess.Write))
            {
                exportForm.ExportFdf(outFdf);
            }
        }

        Console.WriteLine($"Exported PDF fields saved to '{exportFdfPath}'.");
    }
}