using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged_aligned.pdf";

        // Desired uniform margins (in default space units, e.g., points)
        const double leftMargin   = 36;   // 0.5 inch
        const double rightMargin  = 36;
        const double topMargin    = 36;
        const double bottomMargin = 36;

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

        // Create temporary files for the margin‑adjusted PDFs
        string tempFirst  = Path.GetTempFileName();
        string tempSecond = Path.GetTempFileName();

        try
        {
            // Adjust margins of both PDFs so they share the same layout
            PdfFileEditor editor = new PdfFileEditor();

            // Apply margins to all pages (null page array means all pages)
            editor.AddMargins(firstPdf,  tempFirst,  null, leftMargin, rightMargin, topMargin, bottomMargin);
            editor.AddMargins(secondPdf, tempSecond, null, leftMargin, rightMargin, topMargin, bottomMargin);

            // Concatenate the two adjusted PDFs into the final document
            editor.Concatenate(tempFirst, tempSecond, outputPdf);

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try { if (File.Exists(tempFirst))  File.Delete(tempFirst);  } catch { }
            try { if (File.Exists(tempSecond)) File.Delete(tempSecond); } catch { }
        }
    }
}