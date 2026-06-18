using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfImageRemover
{
    // Removes all images from the specified PDF and saves the result.
    public static void RemoveAllImages(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the facade (does not implement IDisposable, so no using block).
        PdfContentEditor editor = new PdfContentEditor();

        // Load the source PDF.
        editor.BindPdf(inputPdfPath);

        // Delete every image in the document.
        editor.DeleteImage();

        // Write the modified PDF to the destination file.
        editor.Save(outputPdfPath);
    }

    // Example entry point.
    public static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        RemoveAllImages(inputPath, outputPath);
        Console.WriteLine($"All images removed. Output saved to '{outputPath}'.");
    }
}