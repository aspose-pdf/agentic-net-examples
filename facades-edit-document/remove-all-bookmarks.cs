using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists; if not, create a minimal placeholder.
        EnsureInputPdfExists(inputPath);

        // Remove all bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteBookmarks();
            editor.Save(outputPath);
        }

        Console.WriteLine($"All bookmarks removed and saved to '{outputPath}'.");
    }

    private static void EnsureInputPdfExists(string path)
    {
        if (!File.Exists(path))
        {
            // Create a simple PDF with a single blank page as a fallback.
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(path);
            }
        }
    }
}