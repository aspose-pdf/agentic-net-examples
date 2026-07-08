using System;
using System.IO;
using System.Drawing;               // Rectangle for the clickable area
using Aspose.Pdf.Facades;           // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string url        = "https://www.example.com";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit the PDF using the Facades API
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the rectangle (left, top, width, height) that will act as the link area
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Create a web link on page 1 (pages are 1‑based)
            editor.CreateWebLink(linkRect, url, 1);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Web link added and saved to '{outputPath}'.");
    }
}