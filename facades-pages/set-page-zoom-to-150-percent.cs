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

        // Verify the document has at least three pages (1‑based indexing).
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than three pages.");
                return;
            }
        }

        // Use PdfPageEditor (a Facade) to set the zoom for page 3.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 3 }; // target page 3 (1‑based)
            editor.Zoom = 1.5f; // 150 % magnification
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 zoom set to 150 % and saved to '{outputPath}'.");
    }
}
