using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for DefaultAppearance

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that will contain the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a DefaultAppearance (font, size, color) for the annotation text
            // Note: the constructor requires System.Drawing.Color
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, da)
            {
                Contents = "Sample free‑text annotation with a callout.",
                Color    = Aspose.Pdf.Color.Yellow // background color of the annotation
            };

            // Set the callout points.
            // Leader line length = 20 points (distance from the annotation rectangle to the first point).
            // The rectangle's right edge is at x = 200, so the first point is placed at x = 220.
            ft.Callout = new Point[]
            {
                new Point(220, 525), // start point (20 points to the right, vertically centered)
                new Point(240, 525), // knee point (bend)
                new Point(260, 525)  // end point (arrow tip)
            };

            // Add the annotation to the page
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation saved to '{outputPath}'.");
    }
}