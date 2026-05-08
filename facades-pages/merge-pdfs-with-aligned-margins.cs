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

        // Temporary files that will hold the PDFs after margin alignment
        const string alignedFirst  = "first_aligned.pdf";
        const string alignedSecond = "second_aligned.pdf";

        // Final merged output
        const string mergedPdf = "merged.pdf";

        // Desired margins (in default space units, points)
        double leftMargin   = 20.0;
        double bottomMargin = 20.0;
        double rightMargin  = 20.0;
        double topMargin    = 20.0;

        // Ensure input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Create a PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Align margins for the first PDF (apply to all pages by passing null for page numbers)
        editor.AddMargins(firstPdf, alignedFirst, null, leftMargin, bottomMargin, rightMargin, topMargin);

        // Align margins for the second PDF
        editor.AddMargins(secondPdf, alignedSecond, null, leftMargin, bottomMargin, rightMargin, topMargin);

        // Concatenate the two aligned PDFs into the final document
        editor.Concatenate(new string[] { alignedFirst, alignedSecond }, mergedPdf);

        // Optional: clean up temporary aligned files
        try
        {
            File.Delete(alignedFirst);
            File.Delete(alignedSecond);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Cleanup warning: {ex.Message}");
        }

        Console.WriteLine($"Merged PDF created at '{mergedPdf}'.");
    }
}