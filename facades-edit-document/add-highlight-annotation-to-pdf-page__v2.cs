using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor with the loaded document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Define the rectangle that covers the text to be highlighted
            // Coordinates are in points (lower‑left X, lower‑left Y, width, height)
            System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);

            // Create a highlight markup annotation on page 3
            // type = 0 (Highlight), contents can be empty, color = Yellow
            editor.CreateMarkup(highlightRect, "", 0, 3, System.Drawing.Color.Yellow);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}