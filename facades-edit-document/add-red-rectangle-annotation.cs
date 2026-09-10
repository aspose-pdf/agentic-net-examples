using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF (will be created if missing)
        const string outputPdf = "output.pdf";  // result PDF with annotation
        const int    pageIndex = 1;             // 1‑based page number to annotate

        // ---------------------------------------------------------------------
        // Ensure a PDF file exists for the editor to bind. In the sandbox there is
        // no pre‑existing file, so we generate a minimal one‑page PDF on‑the‑fly.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdf);
            }
        }

        // Define the annotation rectangle (X, Y, Width, Height) using System.Drawing.Rectangle
        // because PdfContentEditor.CreateSquareCircle expects a System.Drawing.Rectangle.
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 100, 100, 100);

        // Border width: 2 mm ≈ 5.67 points → round to 6 points (int parameter).
        const int borderWidthPoints = 6;

        // Create the content editor, bind the PDF, add a red square (rectangle) annotation,
        // and save the modified document.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                   // Load PDF
            editor.CreateSquareCircle(
                annotRect,                     // location and size (System.Drawing.Rectangle)
                string.Empty,                 // contents (optional)
                System.Drawing.Color.Red,     // border color (System.Drawing.Color)
                true,                         // true = square (rectangle), false = circle
                pageIndex,                    // target page (1‑based)
                borderWidthPoints             // border thickness in points
            );
            editor.Save(outputPdf);                                      // Persist changes
        }

        Console.WriteLine($"Red rectangle annotation added to page {pageIndex} and saved as '{outputPdf}'.");
    }
}
