using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (used by CreateMarkup)
using Aspose.Pdf.Facades;          // Facade API for annotation creation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_highlight.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Define the rectangle that covers the text to be highlighted on page 3
            // (x, y, width, height) – coordinates are in points.
            Rectangle highlightRect = new Rectangle(100, 500, 200, 20);

            // Highlight color (yellow)
            Color highlightColor = Color.Yellow;

            // Markup type: 0 = Highlight
            int markupType = 0;

            // Page number (1‑based indexing)
            int pageNumber = 3;

            // Create the highlight annotation
            editor.CreateMarkup(highlightRect, "Highlighted text", markupType, pageNumber, highlightColor);

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}