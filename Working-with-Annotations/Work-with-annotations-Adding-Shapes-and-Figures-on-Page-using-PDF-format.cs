using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color parameters of the facade methods

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

        // Bind the PDF document using the PdfContentEditor facade.
        // The facade implements IDisposable, so wrap it in a using block.
        using (Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // ------------------------------------------------------------
            // 1. Add a square annotation on page 1.
            //    The rectangle is defined in points (lower‑left origin).
            // ------------------------------------------------------------
            System.Drawing.Rectangle squareRect = new System.Drawing.Rectangle(100, 500, 200, 600);
            // Parameters: rect, contents, color, square (true = square), page, borderWidth
            editor.CreateSquareCircle(squareRect, "Square Annotation", System.Drawing.Color.Blue, true, 1, 3);

            // ------------------------------------------------------------
            // 2. Add a circle annotation on page 1.
            // ------------------------------------------------------------
            System.Drawing.Rectangle circleRect = new System.Drawing.Rectangle(300, 500, 400, 600);
            editor.CreateSquareCircle(circleRect, "Circle Annotation", System.Drawing.Color.Green, false, 1, 3);

            // ------------------------------------------------------------
            // 3. Add a rubber‑stamp annotation on page 1.
            //    An appearance PDF file can be supplied to define the visual look.
            // ------------------------------------------------------------
            System.Drawing.Rectangle stampRect = new System.Drawing.Rectangle(100, 100, 200, 150);
            // Overload: CreateRubberStamp(int page, Rectangle rect, string contents, Color color, string appearanceFile)
            editor.CreateRubberStamp(1, stampRect, "Approved", System.Drawing.Color.Red, "stamp_appearance.pdf");

            // Save the modified document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}