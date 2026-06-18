using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // required for Rectangle and Color in CreateMarkup

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade for editing PDF content
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Define the rectangle (x, y, width, height) for the highlight on page 3
            // Adjust coordinates to match the desired text range.
            Rectangle highlightRect = new Rectangle(100, 500, 200, 50);

            // Create a highlight annotation (type = 0) with yellow color.
            // Parameters: rectangle, contents (optional), type, page number (1‑based), color.
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, Color.Yellow);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}