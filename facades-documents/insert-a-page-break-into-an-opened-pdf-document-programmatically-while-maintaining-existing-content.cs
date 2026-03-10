using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_break.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and prepare a destination PDF.
        using (Document src = new Document(inputPath))
        using (Document dest = new Document())
        {
            // Create a PdfFileEditor instance (no IDisposable required).
            PdfFileEditor editor = new PdfFileEditor();

            // Define where the page break should be inserted.
            // Example: after page 1 at vertical position 500 points.
            PdfFileEditor.PageBreak breakInfo = new PdfFileEditor.PageBreak(1, 500);

            // Add the page break. The method copies content from src to dest
            // and inserts the specified break(s) while preserving existing content.
            editor.AddPageBreak(src, dest, new PdfFileEditor.PageBreak[] { breakInfo });

            // Save the resulting PDF.
            dest.Save(outputPath);
        }

        Console.WriteLine($"Page break inserted. Output saved to '{outputPath}'.");
    }
}