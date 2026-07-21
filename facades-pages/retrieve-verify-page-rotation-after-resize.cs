using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";
        const int pageNumber = 1; // page to inspect (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit the PDF using PdfPageEditor (facade)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Capture the original rotation of the page
            int originalRotation = editor.GetPageRotation(pageNumber);
            Console.WriteLine($"Original rotation of page {pageNumber}: {originalRotation}°");

            // Change the page size (example: A4 landscape – 842×595 points)
            editor.PageSize = new PageSize(842, 595);

            // Apply the size change
            editor.ApplyChanges();

            // Verify that rotation remains unchanged
            int afterResizeRotation = editor.GetPageRotation(pageNumber);
            Console.WriteLine($"Rotation after resizing page {pageNumber}: {afterResizeRotation}°");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}