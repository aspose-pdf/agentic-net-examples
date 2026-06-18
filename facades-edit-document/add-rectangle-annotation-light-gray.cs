using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // PdfContentEditor methods use System.Drawing types

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

        // Use PdfContentEditor (facade) to add a rectangle (square) annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points
            Rectangle rect = new Rectangle(100, 500, 200, 100); // left=100, top=500, width=200, height=100

            // Create a square (rectangle) annotation:
            // - contents: text shown when the annotation is selected
            // - clr: fill color (light gray)
            // - square: true (square/rectangle shape)
            // - page: 1 (first page, 1‑based indexing)
            // - borderWidth: 1 point
            editor.CreateSquareCircle(rect, "Rectangle annotation", Color.LightGray, true, 1, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}