using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // LineAnnotation and LineEnding enums

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure a source PDF exists; create a blank one if missing
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a single blank page
                doc.Save(inputPath);
            }
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            Page page = doc.Pages[1];

            // Annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(280, 530);

            // Create a line annotation and set line ending styles:
            //   - Circle at the start point
            //   - Closed arrow at the end point
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                StartingStyle = LineEnding.Circle,
                EndingStyle   = LineEnding.ClosedArrow,
                // Optional visual styling
                Color          = Color.Blue,
                InteriorColor  = Color.LightGray
            };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}