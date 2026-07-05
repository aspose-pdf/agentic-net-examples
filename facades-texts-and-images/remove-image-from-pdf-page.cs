using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 4;          // target page (1‑based)
        const int imageIndex = 2;          // index of the image to remove on that page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the editor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Delete the specified image from page four
        editor.DeleteImage(pageNumber, new int[] { imageIndex });

        // Save the resulting PDF
        editor.Save(outputPath);

        Console.WriteLine($"Removed image index {imageIndex} from page {pageNumber} and saved to '{outputPath}'.");
    }
}