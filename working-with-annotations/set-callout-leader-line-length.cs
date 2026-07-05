using System;
using System.Drawing; // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free‑text annotation",
                Color    = Aspose.Pdf.Color.Yellow   // Border color of the annotation
            };

            // Leader line length of 20 points: place the first callout point 20 points to the right of the annotation rectangle
            double startX = rect.URX + 20;                         // 20‑point offset from the right edge
            double startY = (rect.LLY + rect.URY) / 2;             // Vertically centered
            double kneeX  = startX + 30;                           // Additional offset for the knee point
            double kneeY  = startY + 30;
            double endX   = kneeX + 30;                            // Final point of the callout line
            double endY   = kneeY;

            // Set the callout points (start, knee, end)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(startX, startY),
                new Aspose.Pdf.Point(kneeX,  kneeY),
                new Aspose.Pdf.Point(endX,   endY)
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the PDF
            doc.Save("FreeTextWithLeaderLine.pdf");
        }

        Console.WriteLine("PDF created with free‑text annotation and 20‑point leader line.");
    }
}