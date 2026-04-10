using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 4;          // target page (1‑based indexing)
        const int    imageId    = 5;          // object ID of the image to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, delete the specified image, and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteImage(pageNumber, new int[] { imageId });
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Removed image ID {imageId} from page {pageNumber} and saved to '{outputPath}'.");
    }
}