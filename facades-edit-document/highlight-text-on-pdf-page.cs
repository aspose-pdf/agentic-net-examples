using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Rectangle and Color for PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the rectangle that covers the text to be highlighted on page 3
        // (example coordinates: x=100, y=500, width=200, height=20)
        Rectangle highlightRect = new Rectangle(100, 500, 200, 20);

        // Create a highlight markup annotation:
        // type = 0 (Highlight), page = 3 (1‑based indexing), color = Yellow
        editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, Color.Yellow);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Highlight annotation applied to page 3 and saved as '{outputPath}'.");
    }
}