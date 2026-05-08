using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color types

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor with the loaded document
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // JavaScript code to display an alert
                string jsCode = "app.alert('Hello from Aspose.PDF!');";

                // Define the clickable area (System.Drawing.Rectangle)
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);

                // Define the rectangle color (System.Drawing.Color)
                System.Drawing.Color rectColor = System.Drawing.Color.Red;

                // Create a JavaScript link annotation on page 1
                editor.CreateJavaScriptLink(jsCode, rect, 1, rectColor);

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"JavaScript annotation added. Saved to '{outputPath}'.");
    }
}