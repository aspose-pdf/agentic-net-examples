using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSanitization provides the cleaning (sanitization) functionality.
            // It implements IDisposable, so we wrap it in a using block for deterministic disposal.
            using (PdfFileSanitization sanitizer = new PdfFileSanitization())
            {
                // Bind the source PDF file that needs to be cleaned.
                sanitizer.BindPdf(inputPath);

                // Optional: enable additional cleanup actions.
                sanitizer.UseRebuildXrefAndTrailer = true; // rebuild cross‑reference table and trailer.
                sanitizer.UseTrimTop = true;               // remove any data before the %PDF header.
                sanitizer.UseTrimBottom = true;            // remove any data after the %%EOF marker.

                // Perform the recovery/sanitization process.
                sanitizer.Recover();

                // Save the cleaned PDF to the desired output file.
                sanitizer.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during sanitization: {ex.Message}");
        }
    }
}