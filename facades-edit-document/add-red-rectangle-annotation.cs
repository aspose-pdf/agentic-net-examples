using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage = 1; // 1‑based page index
        // Rectangle (x, y, width, height) in points. Example values place a 200x100 rectangle at (100,500).
        Rectangle rect = new Rectangle(100, 500, 200, 100);
        // Border width: 2 mm ≈ 5.7 points, rounded to 6.
        int borderWidth = 6;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Create a square (rectangle) annotation with red color.
        editor.CreateSquareCircle(rect, "", Color.Red, true, targetPage, borderWidth);
        editor.Save(outputPath);
        Console.WriteLine($"Red rectangle annotation added to page {targetPage}, saved as '{outputPath}'.");
    }
}