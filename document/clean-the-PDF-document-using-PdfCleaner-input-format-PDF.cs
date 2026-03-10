using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF processing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSanitization provides sanitization and recovery capabilities.
            using (PdfFileSanitization cleaner = new PdfFileSanitization())
            {
                // Bind the source PDF file.
                cleaner.BindPdf(inputPath);

                // Rebuild the cross‑reference table and trailer for a clean structure.
                cleaner.UseRebuildXrefAndTrailer = true;

                // Recover the document (removes corrupted objects, fixes structure).
                cleaner.Recover();

                // Trim any stray data before the PDF header and after the EOF marker.
                cleaner.TrimTop();
                cleaner.TrimBottom();

                // Save the cleaned PDF.
                cleaner.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during cleaning: {ex.Message}");
        }
    }
}