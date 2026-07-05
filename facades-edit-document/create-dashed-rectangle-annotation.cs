using System;
using System.Drawing; // kept for convenience, but types are fully qualified where ambiguous
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string tempPdf = "temp.pdf";
        const string outputPdf = "dashed_rectangle.pdf";

        // STEP 1: Create a simple PDF with one blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();                     // add a blank page
            doc.Save(tempPdf);                   // save the temporary PDF
        }

        // STEP 2: Use PdfContentEditor (Facades) to add a rectangle annotation with a dashed border
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPdf);                  // load the temporary PDF

        // Rectangle coordinates (bottom‑left and top‑right)
        float x1 = 100f;   // left
        float y1 = 500f;   // bottom
        float x2 = 300f;   // right
        float y2 = 600f;   // top

        // Common parameters for all four sides
        int pageNumber = 1;                     // first page
        int borderWidth = 2;                    // border thickness
        System.Drawing.Color lineColor = System.Drawing.Color.Blue; // fully qualified to avoid ambiguity
        string borderStyle = "D";               // "D" = Dashed
        int[] dashPattern = new int[] { 4, 2 }; // 4 units dash, 2 units gap
        string[] lineEndings = new string[] { "None", "None" }; // no arrowheads

        // A dummy rectangle required by the API (its size is irrelevant for line annotations)
        System.Drawing.Rectangle dummyRect = new System.Drawing.Rectangle(0, 0, 0, 0);

        // Top side
        editor.CreateLine(dummyRect, "Top", x1, y2, x2, y2,
            pageNumber, borderWidth, lineColor, borderStyle, dashPattern, lineEndings);
        // Bottom side
        editor.CreateLine(dummyRect, "Bottom", x1, y1, x2, y1,
            pageNumber, borderWidth, lineColor, borderStyle, dashPattern, lineEndings);
        // Left side
        editor.CreateLine(dummyRect, "Left", x1, y1, x1, y2,
            pageNumber, borderWidth, lineColor, borderStyle, dashPattern, lineEndings);
        // Right side
        editor.CreateLine(dummyRect, "Right", x2, y1, x2, y2,
            pageNumber, borderWidth, lineColor, borderStyle, dashPattern, lineEndings);

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Dashed rectangle annotation saved to '{outputPdf}'.");
    }
}
