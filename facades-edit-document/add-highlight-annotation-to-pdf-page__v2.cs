using System;
using System.IO;
using System.Drawing; // for Rectangle and Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle that covers the text to be highlighted on page 3.
            System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 30);

            // Create a highlight markup (type = 0) with yellow color.
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, System.Drawing.Color.Yellow);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}
