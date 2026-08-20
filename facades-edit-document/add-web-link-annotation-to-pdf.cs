using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for System.Drawing.Rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, add a web link, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the clickable area (x, y, width, height) using System.Drawing.Rectangle
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Create a web link on page 1 (the rectangle type must be System.Drawing.Rectangle)
            editor.CreateWebLink(linkRect, url, 1);

            // Persist changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"Web link added and saved to '{outputPath}'.");
    }
}
