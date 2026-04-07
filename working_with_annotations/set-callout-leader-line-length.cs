using System;
using System.Drawing; // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Instantiate the free‑text annotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free‑text annotation",
                Color = Aspose.Pdf.Color.Yellow   // Border color of the annotation
            };

            // Calculate callout points so that the leader line length is exactly 20 points
            // Point 0: anchor point inside the annotation (center‑top of the rectangle)
            double startX = (rect.LLX + rect.URX) / 2;
            double startY = rect.URY; // top edge of the rectangle
            Aspose.Pdf.Point p0 = new Aspose.Pdf.Point(startX, startY);

            // Point 1: knee point – 20 points directly below the start point
            Aspose.Pdf.Point p1 = new Aspose.Pdf.Point(startX, startY - 20);

            // Point 2: end point – same as the knee point (no extension)
            Aspose.Pdf.Point p2 = new Aspose.Pdf.Point(startX, startY - 20);

            // Assign the callout array (exactly three points as required)
            freeText.Callout = new Aspose.Pdf.Point[] { p0, p1, p2 };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the PDF (output format is PDF by default)
            doc.Save("FreeTextWithCallout.pdf");
        }

        Console.WriteLine("PDF with free‑text annotation and 20‑point callout leader line created.");
    }
}