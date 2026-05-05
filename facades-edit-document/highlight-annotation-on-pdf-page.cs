using System;
using System.IO;
using System.Drawing; // for Rectangle and Color
using Aspose.Pdf.Facades; // for PdfContentEditor

namespace HighlightExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Define the rectangle that covers the text to be highlighted on page 3.
            // System.Drawing.Rectangle uses (x, y, width, height).
            // Original Aspose rectangle (llx, lly, urx, ury) = (100, 500, 300, 520)
            // => width = 300 - 100 = 200, height = 520 - 500 = 20
            System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);
            // Choose a highlight color (yellow in this example).
            System.Drawing.Color highlightColor = System.Drawing.Color.Yellow;

            // Create the facade, bind the source PDF, add the highlight markup, and save.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);
            // type = 0 (Highlight), page = 3 (1‑based indexing), contents optional description.
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, highlightColor);
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Highlight annotation added to page 3 and saved as '{outputPath}'.");
        }
    }
}
