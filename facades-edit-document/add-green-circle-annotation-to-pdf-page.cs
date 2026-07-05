using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facades editor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle (x, y, width, height) on page 2
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 200);

            // Create a circle annotation:
            //   rect          – location and size
            //   "Circle"      – contents (tooltip)
            //   Color.Green   – fill color (as required by the API)
            //   false         – false => circle (true would be square)
            //   2             – target page number (1‑based indexing)
            //   3             – border width in points
            editor.CreateSquareCircle(rect, "Circle", System.Drawing.Color.Green, false, 2, 3);

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close(); // optional, releases resources held by the facade
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}