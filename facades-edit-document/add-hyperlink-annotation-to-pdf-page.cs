using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfContentEditor resides here
using System.Drawing;                  // Required for Rectangle and Color types

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // PDF with hyperlink

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define the clickable area on page 2 (left, top, width, height)
        // Note: System.Drawing.Rectangle uses (x, y, width, height)
        Rectangle linkRect = new Rectangle(100, 500, 200, 50);

        // Use PdfContentEditor (implements IDisposable) inside a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a web link that opens the specified URL when the rectangle is clicked
            // Parameters: rectangle, URL, page number (1‑based), optional color (here omitted)
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPdf}'.");
    }
}