using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Source PDF
        const string outputPath = "output_with_js.pdf"; // Destination PDF

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Create the content editor facade and bind the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle area (x, y, width, height) where the link will be active
            // Note: System.Drawing.Rectangle is required by the API.
            Rectangle linkRect = new Rectangle(100, 500, 200, 100);

            // JavaScript code to be executed on click – shows an alert dialog
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Create the JavaScript link on page 1 with a visible rectangle color (e.g., Red)
            editor.CreateJavaScriptLink(jsCode, linkRect, 1, Color.Red);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript link: {outputPath}");
    }
}