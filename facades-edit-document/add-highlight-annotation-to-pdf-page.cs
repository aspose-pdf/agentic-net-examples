using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for Rectangle and Color (PdfContentEditor expects System.Drawing types)

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

        // Define the rectangle that covers the text range on page 3.
        // Coordinates are in points (1/72 inch) from the lower‑left corner of the page.
        // Adjust the values (llx, lly, width, height) to match the actual text location.
        Rectangle highlightRect = new Rectangle(100, 500, 200, 20); // example values

        // Use PdfContentEditor (a Facades class) to add the highlight annotation.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Create a highlight markup annotation.
            // type = 0  => Highlight
            // page = 3  => third page (1‑based indexing)
            // color = Yellow (System.Drawing.Color is required by the API)
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, Color.Yellow);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}