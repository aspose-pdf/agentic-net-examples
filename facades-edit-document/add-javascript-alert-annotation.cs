using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Rectangle and System.Drawing.Color used by CreateJavaScriptLink

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // JavaScript code to be executed on activation
                string jsCode = "app.alert('Hello from Aspose.Pdf!');";

                // Define the clickable rectangle (x, y, width, height) using System.Drawing.Rectangle
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 100, 200);

                // Create the JavaScript link on page 1 with a red border using System.Drawing.Color
                editor.CreateJavaScriptLink(jsCode, rect, 1, System.Drawing.Color.Red);

                // Save the modified PDF to a new file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"JavaScript annotation added. Saved to '{outputPath}'.");
    }
}
