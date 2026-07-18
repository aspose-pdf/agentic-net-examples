using System;
using System.IO;
using System.Drawing;               // needed for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the rectangle (in points) that covers the text to be highlighted on page 3
            // PdfContentEditor.CreateMarkup expects a System.Drawing.Rectangle and System.Drawing.Color
            System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);

            // Create a highlight markup annotation (type = 0) on page 3 with yellow color
            editor.CreateMarkup(highlightRect, "Highlighted text", 0, 3, System.Drawing.Color.Yellow);

            // Save the modified PDF using the facade's Save method
            editor.Save(outputPath);
            editor.Close(); // optional, releases resources held by the facade
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}
