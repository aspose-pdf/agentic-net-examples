using System;
using System.IO;
using System.Drawing;               // System.Drawing.Rectangle and System.Drawing.Color are required by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the rectangle that covers the text to be highlighted on page 3.
        // Coordinates are in points (1 point = 1/72 inch). Adjust as needed.
        // Rectangle(x, y, width, height)
        System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);

        // Use a using block for deterministic disposal of the facade (lifecycle rule)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document (load rule)
            editor.BindPdf(inputPath);

            // Create a highlight markup annotation:
            // type = 0 (Highlight), page = 3 (one‑based indexing), color = Yellow
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, System.Drawing.Color.Yellow);

            // Save the modified PDF (save rule)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}