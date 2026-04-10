using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Removes all images from a PDF file and saves the result to a new file.
    static void RemoveAllImages(string inputPdfPath, string outputPdfPath)
    {
        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfContentEditor is a Facade class that can edit PDF content.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdfPath);

            // Delete all images from the document.
            editor.DeleteImage();

            // Save the modified PDF to the specified output path.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPdfPath}'.");
    }

    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = input PDF path, args[1] = output PDF path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: RemoveImages <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        RemoveAllImages(inputPath, outputPath);
    }
}