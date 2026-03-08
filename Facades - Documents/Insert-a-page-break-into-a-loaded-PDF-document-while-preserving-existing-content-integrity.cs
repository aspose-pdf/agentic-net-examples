using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with the inserted page break
        const string sourcePath = "input.pdf";
        const string outputPath = "output_with_break.pdf";

        // Page number (1‑based) where the break will be inserted
        const int breakPageNumber = 2;

        // Vertical position (in points) of the break on the specified page.
        // 0 = bottom of the page, increasing upwards.
        const double breakPosition = 400.0;

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the source document and prepare an empty destination document.
        // Both documents are wrapped in using blocks for deterministic disposal.
        using (Document srcDoc = new Document(sourcePath))
        using (Document destDoc = new Document())
        {
            // Define the page break to be added. The PageBreak class is nested inside PdfFileEditor.
            PdfFileEditor.PageBreak[] breaks = new PdfFileEditor.PageBreak[]
            {
                new PdfFileEditor.PageBreak(breakPageNumber, breakPosition)
            };

            // Use PdfFileEditor to add the page break.
            PdfFileEditor editor = new PdfFileEditor();
            editor.AddPageBreak(srcDoc, destDoc, breaks);

            // Save the resulting document.
            destDoc.Save(outputPath);
        }

        Console.WriteLine($"Page break inserted. Output saved to '{outputPath}'.");
    }
}
