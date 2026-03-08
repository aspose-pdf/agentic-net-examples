using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor facade to add annotations.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 1. Add a rubber stamp annotation.
            // -----------------------------------------------------------------
            // Rectangle defining the stamp position (x, y, width, height).
            // Use fully qualified System.Drawing.Rectangle to avoid ambiguity.
            System.Drawing.Rectangle stampRect = new System.Drawing.Rectangle(100, 500, 150, 50);

            // Icon name (e.g., "Draft") and the text that appears inside the stamp.
            string stampIcon   = "Draft";
            string stampText   = "Approved";

            // Color of the stamp. Use System.Drawing.Color (the API expects this type).
            System.Drawing.Color stampColor = System.Drawing.Color.Red;

            // CreateRubberStamp overload: (int page, Rectangle rect, string icon,
            //                               string contents, Color color)
            editor.CreateRubberStamp(
                page: 1,                     // 1‑based page index
                annotRect: stampRect,
                icon: stampIcon,
                annotContents: stampText,
                color: stampColor);

            // -----------------------------------------------------------------
            // 2. Add a free‑text annotation.
            // -----------------------------------------------------------------
            System.Drawing.Rectangle freeTextRect = new System.Drawing.Rectangle(200, 400, 200, 100);
            string freeTextContents = "Review this section.";

            // CreateFreeText overload: (Rectangle rect, string contents, int page)
            editor.CreateFreeText(
                rect: freeTextRect,
                contents: freeTextContents,
                page: 1);

            // -----------------------------------------------------------------
            // Save the modified document.
            // -----------------------------------------------------------------
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPdf}'.");
    }
}
