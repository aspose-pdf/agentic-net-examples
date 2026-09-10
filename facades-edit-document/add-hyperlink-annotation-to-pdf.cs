using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page where the link rectangle will be placed (1‑based)
        const int originalPage = 1;
        // Destination page number (1‑based)
        const int destinationPage = 3;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade for editing PDF content
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Define the clickable area (x, y, width, height)
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Create a local link that jumps from originalPage to destinationPage
            editor.CreateLocalLink(linkRect, destinationPage, originalPage);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added. Saved to '{outputPath}'.");
    }
}