using System;
using System.IO;
using System.Drawing;               // needed for System.Drawing.Rectangle and Color
using Aspose.Pdf;                  // core PDF types
using Aspose.Pdf.Facades;          // PdfContentEditor facade

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

        try
        {
            // Bind the PDF to the editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // JavaScript code to be executed on click
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Define the clickable rectangle (x, y, width, height) using System.Drawing.Rectangle
            System.Drawing.Rectangle clickRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create the JavaScript link on page 1 with a visible border color (System.Drawing.Color)
            editor.CreateJavaScriptLink(jsCode, clickRect, 1, System.Drawing.Color.Red);

            // Save the modified document
            editor.Save(outputPath);
            editor.Close(); // optional, releases resources held by the facade

            Console.WriteLine($"JavaScript annotation added. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
