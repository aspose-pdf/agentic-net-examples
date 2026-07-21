using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment and Position

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string alignedFirst = "first_aligned.pdf";
        const string alignedSecond = "second_aligned.pdf";
        const string outputPdf = "merged_aligned.pdf";

        // ---------------------------------------------------------------------
        // 1. Create placeholder PDFs so the example can run in a clean sandbox.
        // ---------------------------------------------------------------------
        CreateSamplePdf(firstPdf, "First PDF – Sample page");
        CreateSamplePdf(secondPdf, "Second PDF – Sample page");

        // Desired uniform margins (in points)
        double leftMargin = 20;
        double rightMargin = 20;
        double topMargin = 20;
        double bottomMargin = 20;

        // PdfFileEditor provides methods for margin adjustment and concatenation
        PdfFileEditor editor = new PdfFileEditor();

        // Apply the same margins to all pages of each source PDF
        // Passing null for the pages array applies the margins to every page
        editor.AddMargins(firstPdf, alignedFirst, null, leftMargin, rightMargin, topMargin, bottomMargin);
        editor.AddMargins(secondPdf, alignedSecond, null, leftMargin, rightMargin, topMargin, bottomMargin);

        // Concatenate the two margin‑aligned PDFs into a single document
        editor.Concatenate(alignedFirst, alignedSecond, outputPdf);

        // Clean up temporary files (optional)
        TryDelete(alignedFirst);
        TryDelete(alignedSecond);
        TryDelete(firstPdf);
        TryDelete(secondPdf);

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }

    // Helper to create a minimal PDF with a single page and some text.
    private static void CreateSamplePdf(string path, string text)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a simple text fragment so the page is not completely empty
            TextFragment tf = new TextFragment(text)
            {
                // Position the text roughly in the centre of the page
                Position = new Position(100, 700)
            };
            page.Paragraphs.Add(tf);

            doc.Save(path);
        }
    }

    // Helper to delete a file without throwing if it does not exist.
    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch
        {
            // Swallow any exception – cleanup is non‑critical.
        }
    }
}
