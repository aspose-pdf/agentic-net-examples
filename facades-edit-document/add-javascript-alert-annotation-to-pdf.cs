using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for Rectangle and Color used by CreateJavaScriptLink

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, add a JavaScript link annotation, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height) in points.
            // CreateJavaScriptLink expects a System.Drawing.Rectangle, so we use that type explicitly.
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // JavaScript code to be executed on click.
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Create the JavaScript link on page 1 with a red border.
            // The border color must also be a System.Drawing.Color.
            editor.CreateJavaScriptLink(jsCode, linkRect, 1, System.Drawing.Color.Red);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript annotation saved to '{outputPath}'.");
    }
}
