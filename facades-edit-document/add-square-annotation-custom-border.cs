using System;
using System.IO;
using System.Drawing;               // Required by PdfContentEditor for Rectangle and Color
using Aspose.Pdf.Facades;          // Facade API for annotation creation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (x, y, width, height) in points
        Rectangle rect = new Rectangle(100, 500, 200, 150);

        // Create a square annotation with custom border thickness of 3 points
        // Parameters: rectangle, contents, color, square(true), page number, border width
        editor.CreateSquareCircle(rect, "Custom border annotation", Color.Blue, true, 1, 3);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}