using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable via SaveableFacade, so use using for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document to be edited
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height) in points
            Rectangle rect = new Rectangle(100, 700, 200, 100);

            // JavaScript code that will be executed on click
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Create a JavaScript link on page 1; the rectangle will be highlighted in red
            editor.CreateJavaScriptLink(jsCode, rect, 1, Color.Red);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript link added. Saved to '{outputPath}'.");
    }
}