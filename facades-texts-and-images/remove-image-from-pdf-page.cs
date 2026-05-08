using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 4;               // Target page (1‑based indexing)
        int[] imageIndexes = new int[] { 2 };   // Index of the image to remove on page 4 (replace with actual index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Delete the specified image(s) from page 4
            editor.DeleteImage(pageNumber, imageIndexes);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Specified image removed. Output saved to '{outputPath}'.");
    }
}