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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Capture the original dimensions of page 8
        // ------------------------------------------------------------
        double originalWidth, originalHeight;
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF file to the facade
            editor.BindPdf(inputPath);

            // Get the size of page 8 (pages are 1‑based)
            PageSize size = editor.GetPageSize(8);
            originalWidth = size.Width;
            originalHeight = size.Height;
        }

        // ------------------------------------------------------------
        // Step 2: Load the document, modify page 8, then revert size
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually has eight pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("Document contains fewer than 8 pages.");
                return;
            }

            Page page8 = doc.Pages[8];

            // Example modification: change to A4 size (595 x 842 points)
            page8.SetPageSize(595, 842);

            // Revert to the originally stored dimensions
            page8.SetPageSize(originalWidth, originalHeight);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 8 size restored and saved to '{outputPath}'.");
    }
}