using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "imageEdited.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create and use PdfContentEditor (it implements IDisposable)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(sourcePath);

            // Replace images on specific pages.
            // Page numbers are 1‑based, image indexes are 0‑based.
            // Ensure the replacement image files exist before running.
            editor.ReplaceImage(1, 0, "newImage1.jpg"); // first image on page 1
            editor.ReplaceImage(1, 1, "newImage2.png"); // second image on page 1 (if present)
            editor.ReplaceImage(2, 0, "newImage3.jpg"); // first image on page 2

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}