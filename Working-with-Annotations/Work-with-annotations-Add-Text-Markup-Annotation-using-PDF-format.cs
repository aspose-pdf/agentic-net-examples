using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (PdfContentEditor API)
using Aspose.Pdf.Facades;          // Facade for annotation operations

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor and add a highlight annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (coordinates are in points)
            // Example rectangle: lower‑left (100,500), upper‑right (300,520)
            Rectangle rect = new Rectangle(100, 500, 300, 520);

            string contents = "Highlighted text"; // Optional tooltip text
            int type = 0;                         // 0 = Highlight (per API)
            int page = 1;                         // 1‑based page index
            Color color = Color.Yellow;           // Highlight colour (System.Drawing.Color)

            // Create the highlight (text markup) annotation
            editor.CreateMarkup(rect, contents, type, page, color);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}