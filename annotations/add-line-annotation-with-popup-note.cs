using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (adjust index as needed)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a LineAnnotation
            // ------------------------------------------------------------
            // Define the annotation rectangle (position on the page)
            Rectangle lineRect = new Rectangle(100, 700, 300, 650);

            // Create the line annotation – constructor requires the start and end points
            LineAnnotation lineAnno = new LineAnnotation(
                page,
                lineRect,
                new Point(100, 700), // start (top‑left of the rectangle)
                new Point(300, 650)  // end   (bottom‑right of the rectangle)
            );

            // Appearance settings
            lineAnno.Color = Color.Red;                     // line colour
            lineAnno.Contents = "Important relationship"; // tooltip text
            lineAnno.Title = "Line Note";                  // optional title
            // Optional: define arrow heads
            lineAnno.StartingStyle = LineEnding.None;
            lineAnno.EndingStyle = LineEnding.OpenArrow;

            // ------------------------------------------------------------
            // 2. Create a PopupAnnotation linked to the LineAnnotation
            // ------------------------------------------------------------
            // Define a rectangle where the popup will appear
            Rectangle popupRect = new Rectangle(320, 720, 520, 620);
            PopupAnnotation popupAnno = new PopupAnnotation(page, popupRect)
            {
                // Link the popup to its parent annotation
                Parent = lineAnno,
                // Set the popup's note text
                Contents = "This is a detailed note attached to the line annotation.",
                // The Title property is not available on PopupAnnotation in this version, so we omit it.
                Open = false // optional – start closed
            };

            // ------------------------------------------------------------
            // 3. Add annotations to the page
            // ------------------------------------------------------------
            page.Annotations.Add(lineAnno);
            page.Annotations.Add(popupAnno);

            // ------------------------------------------------------------
            // 4. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with line and popup annotations to '{outputPath}'.");
    }
}
