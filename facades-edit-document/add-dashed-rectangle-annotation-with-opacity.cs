using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;                         // Document, Page, Color
using Aspose.Pdf.Facades;                 // PdfContentEditor
using Aspose.Pdf.Annotations;             // Annotation, Border, Dash

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

        // Load PDF with PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define rectangle area (x, y, width, height) – System.Drawing.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a square (rectangle) annotation on page 6
            // Parameters: rect, contents, border color, square=true, page number, border width (int)
            editor.CreateSquareCircle(rect, "", System.Drawing.Color.Black, true, 6, 2);

            // Retrieve the newly added annotation (last one on the page)
            Page page = editor.Document.Pages[6];               // 1‑based page index
            Annotation annotation = page.Annotations[page.Annotations.Count - 1];

            // Configure dashed border
            Border border = new Border(annotation);
            border.Dash = new Dash(new int[] { 3, 3 });          // dash pattern
            annotation.Border = border;

            // Set 50% opacity – use ARGB where alpha = 128 (≈50%)
            // Aspose.Pdf.Color.FromArgb expects 4 components: alpha, red, green, blue
            annotation.Color = Aspose.Pdf.Color.FromArgb(128, 0, 0, 0);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
