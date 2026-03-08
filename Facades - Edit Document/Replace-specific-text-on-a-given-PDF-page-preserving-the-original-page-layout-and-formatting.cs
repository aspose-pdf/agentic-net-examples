using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, text to replace, replacement text, and target page (1‑based)
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "Hello World";
        const string destText   = "Hi Universe";
        const int    pageNumber = 1; // replace on first page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade that can edit PDF content
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace the specified text on the given page.
            // No TextState is supplied, so the original font, size, color, etc. are preserved.
            bool replaced = editor.ReplaceText(srcText, pageNumber, destText);

            if (replaced)
                Console.WriteLine($"Text '{srcText}' was replaced with '{destText}' on page {pageNumber}.");
            else
                Console.WriteLine($"Text '{srcText}' not found on page {pageNumber}.");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}