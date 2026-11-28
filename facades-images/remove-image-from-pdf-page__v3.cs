using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create PdfContentEditor (does not implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF file
        editor.BindPdf(inputPath);

        // Define a rectangle around the image location (350,350) to (360,360)
        // DeleteImage expects an int[] with four coordinates: left, bottom, right, top
        int[] rect = new int[] { 350, 350, 360, 360 };

        // Delete the image on page 40 that intersects the rectangle
        editor.DeleteImage(40, rect);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Image removed and saved to '{outputPath}'.");
    }
}
