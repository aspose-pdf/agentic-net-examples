using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle (and Color if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to add a web link on page 2
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height)
            // Adjust coordinates as needed for your document
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Create a web link that opens https://example.com when clicked
            // Parameters: rectangle, URL, originalPage (1‑based)
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}