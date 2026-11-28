using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string newImagePath = "newImage.jpg";
        const string outputPath = "revised.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so no using block is needed.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the source PDF.
        editor.BindPdf(sourcePath);

        // Replace the first image on the first page with the new image.
        // Page numbers and image indexes are 1‑based.
        editor.ReplaceImage(1, 1, newImagePath);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}