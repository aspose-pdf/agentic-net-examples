using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the image to be removed
        const string inputPath = "input.pdf";
        // Output PDF after the image has been removed
        const string outputPath = "output.pdf";
        // Page number (1‑based) from which the image will be removed
        const int pageNumber = 4;
        // Object ID of the image to remove – replace with the actual ID
        const int imageObjectId = 12345;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor which provides DeleteImage that works with object IDs
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Delete the image identified by its object ID from the specified page
        editor.DeleteImage(pageNumber, new int[] { imageObjectId });

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Removed image (Object ID {imageObjectId}) from page {pageNumber}.");
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}
