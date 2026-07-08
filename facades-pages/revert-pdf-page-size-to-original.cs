using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to work with page sizes.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Store the original size of page 8 (1‑based indexing).
            PageSize originalSize = editor.GetPageSize(8);
            double originalWidth  = originalSize.Width;
            double originalHeight = originalSize.Height;

            // Example modification: change page 8 size to A4.
            editor.Document.Pages[8].SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // ... other processing could happen here ...

            // Revert page 8 back to its original dimensions.
            editor.Document.Pages[8].SetPageSize(originalWidth, originalHeight);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}