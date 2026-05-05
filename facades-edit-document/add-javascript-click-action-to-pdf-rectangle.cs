using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color (method signature)
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor, bind the PDF, add a JavaScript link, and save
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // JavaScript to be executed on click
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Define the clickable rectangle (x, y, width, height) in points
            Rectangle rect = new Rectangle(100, 500, 200, 100);

            // Create the JavaScript link on page 1 with a visible red border
            editor.CreateJavaScriptLink(jsCode, rect, 1, Color.Red);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript link added. Saved to '{outputPath}'.");
    }
}