using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Rectangle and System.Drawing.Color used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Example rectangle coordinates for the highlight (adjust as needed)
        int x      = 100;   // lower‑left X (System.Drawing.Rectangle uses X as left)
        int y      = 500;   // lower‑left Y (System.Drawing.Rectangle uses Y as top, but PdfContentEditor expects lower‑left; values are passed directly)
        int width  = 200;   // rectangle width
        int height = 20;    // rectangle height

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a highlight annotation on page 3, and save.
        using (Document doc = new Document(inputPath))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the document to the facade.
            editor.BindPdf(doc);

            // Create a highlight markup (type = 0) on page 3.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, y, width, height);
            editor.CreateMarkup(rect, "Highlighted text", 0, 3, System.Drawing.Color.Yellow);

            // Persist the changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added to page 3 and saved as '{outputPath}'.");
    }
}
