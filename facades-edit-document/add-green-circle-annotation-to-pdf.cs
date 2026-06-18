using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
            var rect = new System.Drawing.Rectangle(100, 500, 100, 100);

            // Add a circle annotation on page 2:
            // - empty contents
            // - green fill color
            // - not a square (false)
            // - border width 3 points (int required by API)
            editor.CreateSquareCircle(
                rect,
                string.Empty,
                System.Drawing.Color.Green,
                false,
                2,
                3); // line width must be an int, not a float

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
