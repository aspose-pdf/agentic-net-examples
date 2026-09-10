using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";

        // Temporary files that will hold the PDFs after margin alignment
        const string firstAligned  = "first_aligned.pdf";
        const string secondAligned = "second_aligned.pdf";

        // Final merged output
        const string mergedPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define uniform margins (in default PDF units, i.e., points)
        double leftMargin   = 20.0;
        double bottomMargin = 20.0;
        double rightMargin  = 20.0;
        double topMargin    = 20.0;

        // Create a PdfFileEditor instance (no IDisposable implementation required)
        PdfFileEditor editor = new PdfFileEditor();

        // Align margins for the first PDF
        // Passing null for the pages array applies the margins to all pages
        editor.AddMargins(firstPdf, firstAligned, null, leftMargin, bottomMargin, rightMargin, topMargin);

        // Align margins for the second PDF
        editor.AddMargins(secondPdf, secondAligned, null, leftMargin, bottomMargin, rightMargin, topMargin);

        // Concatenate the two aligned PDFs into the final document
        editor.Concatenate(firstAligned, secondAligned, mergedPdf);

        // Optional: clean up temporary files
        try { File.Delete(firstAligned); } catch { /* ignore cleanup errors */ }
        try { File.Delete(secondAligned); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF saved to '{mergedPdf}'.");
    }
}