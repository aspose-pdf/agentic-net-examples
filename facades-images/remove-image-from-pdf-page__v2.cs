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

        // PdfContentEditor does not implement IDisposable; do not wrap in using.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Define the rectangle that covers the image at (150,150) on page 5.
        // DeleteImage expects an int[]: {left, bottom, right, top}.
        int[] imageRect = new int[] { 150, 150, 151, 151 };

        // Delete the image on page 5 (pages are 1‑based).
        editor.DeleteImage(5, imageRect);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Image removed and saved to '{outputPath}'.");
    }
}
