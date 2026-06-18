using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";
        // Page number (1‑based) where the annotation will be placed
        const int pageNumber = 1;

        // Define the annotation rectangle (coordinates are in points).
        // Example: lower‑left (100, 500), width 200, height 100.
        // System.Drawing.Rectangle is required by the API.
        var rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Annotation contents (optional, can be empty)
        const string contents = "Red rectangle annotation";

        // Border thickness: 2 mm ≈ 6 points (integer value)
        const int borderWidth = 6;

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor to add the square (rectangle) annotation
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Create a square annotation (square = true) with red color
        editor.CreateSquareCircle(
            rect,                     // rectangle defining position and size
            contents,                 // annotation text
            System.Drawing.Color.Red, // border color
            true,                     // true => square (rectangle), false => circle
            pageNumber,               // target page (1‑based)
            borderWidth);             // border thickness in points

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}