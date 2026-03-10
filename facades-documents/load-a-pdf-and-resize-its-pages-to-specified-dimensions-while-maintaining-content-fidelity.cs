using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        // Desired page dimensions in points (1 point = 1/72 inch)
        // Example: A4 size = 595 x 842 points
        // PageSize constructor expects float values, so use float literals (or cast)
        const float newWidth = 595f;   // width
        const float newHeight = 842f;  // height

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a Facades class) to change page size while preserving content
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF to the editor
            editor.BindPdf(inputPath);

            // Set the new page size (width, height) using Aspose.Pdf.PageSize
            editor.PageSize = new Aspose.Pdf.PageSize(newWidth, newHeight);

            // Keep the original content scaling (100%) – Zoom property expects a float
            editor.Zoom = 1.0f;

            // Apply the changes to all pages
            editor.ApplyChanges();

            // Save the modified PDF to the destination path
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
