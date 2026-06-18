using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facades for stamping and editing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain page dimensions (needed for centering)
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            // Page 8 (1‑based indexing)
            Page targetPage = doc.Pages[8];
            double pageWidth  = targetPage.PageInfo.Width;
            double pageHeight = targetPage.PageInfo.Height;

            // Bind the PDF to the content editor (facade)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Move the first stamp on page 8 to the centre.
            // Stamp index is 1‑based; adjust if a different stamp is required.
            int stampIndex = 1;
            double newX = pageWidth  / 2.0; // horizontal centre
            double newY = pageHeight / 2.0; // vertical centre

            editor.MoveStamp(8, stampIndex, newX, newY);

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}