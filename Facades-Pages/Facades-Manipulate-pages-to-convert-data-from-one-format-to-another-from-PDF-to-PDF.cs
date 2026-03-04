using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Extract a range of pages (2‑4) from the source PDF.
            // ------------------------------------------------------------
            PdfFileEditor fileEditor = new PdfFileEditor();
            string extractedPdf = "extracted.pdf";
            fileEditor.Extract(inputPdf, 2, 4, extractedPdf);
            Console.WriteLine($"Pages 2‑4 extracted to '{extractedPdf}'.");

            // ------------------------------------------------------------
            // 2. Create a booklet from the extracted PDF.
            // ------------------------------------------------------------
            string bookletPdf = "booklet.pdf";
            bool bookletCreated = fileEditor.MakeBooklet(extractedPdf, bookletPdf);
            Console.WriteLine(bookletCreated
                ? $"Booklet created at '{bookletPdf}'."
                : "Failed to create booklet.");

            // ------------------------------------------------------------
            // 3. Split the original PDF into single‑page PDFs.
            // ------------------------------------------------------------
            string splitDir = "output_pages";
            Directory.CreateDirectory(splitDir);
            // %NUM% will be replaced with the page number (1‑based).
            string splitTemplate = Path.Combine(splitDir, "page%NUM%.pdf");
            fileEditor.SplitToPages(inputPdf, splitTemplate);
            Console.WriteLine($"Document split into individual pages under '{splitDir}'.");

            // ------------------------------------------------------------
            // 4. Rotate all pages of the original PDF by 90 degrees.
            // ------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(inputPdf);
            pageEditor.Rotation = 90;               // valid values: 0, 90, 180, 270
            pageEditor.ApplyChanges();              // apply the rotation
            string rotatedPdf = "rotated.pdf";
            pageEditor.Save(rotatedPdf);
            pageEditor.Close();                     // release the bound document
            Console.WriteLine($"All pages rotated and saved to '{rotatedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}