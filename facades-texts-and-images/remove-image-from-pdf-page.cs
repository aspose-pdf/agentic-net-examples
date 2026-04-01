using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 4;
        const int imageObjectId = 5; // replace with the actual image object ID to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor and bind the PDF file
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Remove the image with the specified object ID from the given page
        // DeleteImage is the available method; it accepts an array of image indexes (object IDs)
        editor.DeleteImage(pageNumber, new int[] { imageObjectId });

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Image with object ID {imageObjectId} removed from page {pageNumber}. Saved to '{outputPath}'.");
    }
}