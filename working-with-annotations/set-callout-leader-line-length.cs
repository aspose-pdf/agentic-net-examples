using System;
using System.IO;
using System.Drawing; // needed for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "FreeTextCallout.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle that will contain the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a DefaultAppearance (font, size, color) for the annotation text
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Note with 20‑pt leader line",
                Color = Aspose.Pdf.Color.Yellow // background color of the annotation
            };

            // Set the Callout points so that the leader line length is 20 points.
            // Aspose.Pdf.Point[0] – start point inside the annotation (approximate centre).
            // Aspose.Pdf.Point[1] – knee point 20 points away from the start (defines leader line length).
            // Aspose.Pdf.Point[2] – end point (same as knee for a straight leader line).
            ft.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(200, 625), // start inside annotation
                new Aspose.Pdf.Point(200, 605), // knee point 20 points below start
                new Aspose.Pdf.Point(200, 605)  // end point (straight line)
            };

            // Add the annotation to the page
            page.Annotations.Add(ft);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}