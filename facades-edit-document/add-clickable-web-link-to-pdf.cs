using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

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

        // Edit the PDF using PdfContentEditor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the clickable area (x, y, width, height)
            Rectangle rect = new Rectangle(100, 500, 200, 50);

            // Add a web link on the first page
            editor.CreateWebLink(rect, url, 1);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Web link added and saved to '{outputPath}'.");
    }
}