using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 4;      // target page (1‑based)
        const int imageObjectId = 2;   // replace with the actual image index to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so we manage it manually
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Remove the specified image from page four
        editor.DeleteImage(pageNumber, new int[] { imageObjectId });

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Removed image ID {imageObjectId} from page {pageNumber} and saved to '{outputPath}'.");
    }
}