using System;
using System.IO;
using System.Drawing;               // Rectangle and Color are defined here
using Aspose.Pdf.Facades;          // PdfContentEditor resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // PDF with hyperlink annotation
        const int destinationPage = 3;           // page to jump to (1‑based)
        const int originalPage    = 1;           // page where the link will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the clickable rectangle on the original page.
        // Rectangle(x, y, width, height) – coordinates are in points.
        Rectangle linkRect = new Rectangle(100, 700, 150, 30);

        // Use PdfContentEditor (facade) to add a local link.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);                                 // load PDF
            editor.CreateLocalLink(linkRect, destinationPage, originalPage); // add link
            editor.Save(outputPath);                                   // save result
        }

        Console.WriteLine($"Hyperlink annotation added. Saved to '{outputPath}'.");
    }
}