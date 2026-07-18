using System;
using System.IO;
using System.Drawing;               // Rectangle and Color are defined here
using Aspose.Pdf.Facades;          // PdfContentEditor resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string url        = "https://www.example.com";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, create a web link, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the clickable area (x, y, width, height) on page 1
            Rectangle linkRect = new Rectangle(100, 700, 200, 50);

            // Create a web link that opens the specified URL
            editor.CreateWebLink(linkRect, url, 1);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Web link added and saved to '{outputPath}'.");
    }
}