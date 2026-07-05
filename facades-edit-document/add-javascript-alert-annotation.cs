using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color types used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to add a JavaScript link annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the clickable area (coordinates are in points)
            Rectangle linkRect = new Rectangle(100, 500, 200, 100);

            // JavaScript code to be executed on click
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Create the JavaScript link on page 1 with a red border
            editor.CreateJavaScriptLink(jsCode, linkRect, 1, Color.Red);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript annotation added. Saved to '{outputPath}'.");
    }
}