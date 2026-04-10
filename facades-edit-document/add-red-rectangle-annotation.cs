using System;
using System.Drawing; // Rectangle and Color (kept for other potential uses)
using System.IO; // File.Exists
using Aspose.Pdf; // Document
using Aspose.Pdf.Facades; // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // result PDF
        const int pageIndex = 1;                // 1‑based page number where the annotation will be placed

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple one‑page PDF.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a new empty document with a single blank page.
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Define the annotation rectangle (coordinates are in points).
        // Example: lower‑left corner (100, 500), width 200, height 200.
        // Use fully‑qualified System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 200);

        // Border width: 2 mm ≈ 5.67 points → round to 6 points (int)
        int borderWidth = 6;

        // Create a red square (rectangle) annotation.
        // Parameters: rect, contents, colour, square(true), page number, border width.
        // Use fully‑qualified System.Drawing.Color for the same reason.
        editor.CreateSquareCircle(
            rect,
            "Red rectangle annotation",
            System.Drawing.Color.Red,
            true,          // true = square (rectangle), false = circle
            pageIndex,
            borderWidth);

        // Save the modified PDF
        editor.Save(outputPdf);
    }
}
