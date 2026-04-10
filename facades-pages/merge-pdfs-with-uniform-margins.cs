using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";

        // Temporary files after applying uniform margins
        const string firstAligned  = "first_aligned.pdf";
        const string secondAligned = "second_aligned.pdf";

        // Final merged output
        const string mergedPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        // Desired uniform margins (in default PDF units, 1/72 inch)
        double leftMargin   = 36; // 0.5 inch
        double rightMargin  = 36;
        double topMargin    = 36;
        double bottomMargin = 36;

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Apply margins to the first PDF (null page array means all pages)
        editor.AddMargins(firstPdf, firstAligned, null, leftMargin, rightMargin, topMargin, bottomMargin);

        // Apply margins to the second PDF
        editor.AddMargins(secondPdf, secondAligned, null, leftMargin, rightMargin, topMargin, bottomMargin);

        // Concatenate the two margin‑aligned PDFs into the final document
        editor.Concatenate(firstAligned, secondAligned, mergedPdf);

        // Optional: clean up temporary aligned files
        try { File.Delete(firstAligned); } catch { /* ignore */ }
        try { File.Delete(secondAligned); } catch { /* ignore */ }

        Console.WriteLine($"Merged PDF created at '{mergedPdf}'.");
    }
}