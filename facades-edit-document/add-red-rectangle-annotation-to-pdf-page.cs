using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Source PDF
        const string outputPath = "output.pdf";  // Destination PDF
        const int    pageNumber = 1;             // Target page (1‑based indexing)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the annotation rectangle in points (1 point = 1/72 inch).
        // Example coordinates: lower‑left corner (100,500) with width 200 and height 100.
        Rectangle annotRect = new Rectangle(100, 500, 300, 600); // x, y, width, height

        // Convert 2 mm border thickness to points.
        // 1 mm = 72 pt / 25.4 ≈ 2.8346 pt → 2 mm ≈ 5.669 pt → round to nearest integer.
        int borderWidth = (int)Math.Round(2.0 * 72.0 / 25.4); // ≈6 points

        // Create the editor, bind the PDF, add a red square (rectangle) annotation, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Parameters: rectangle, contents, color, square(true for rectangle), page, border width.
        editor.CreateSquareCircle(
            annotRect,
            "Red rectangle annotation",
            Color.Red,
            true,          // true => square (rectangle) shape
            pageNumber,
            borderWidth);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}