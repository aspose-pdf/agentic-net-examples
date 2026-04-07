using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_arrow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF (lifecycle rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(280, 530);

            // Create a line annotation on the page
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                // Set the line color (cross‑platform Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.Blue,

                // Set the ending style to a closed arrow for direction indication
                EndingStyle = LineEnding.ClosedArrow,

                // Optionally set the starting style as well (e.g., no arrow)
                StartingStyle = LineEnding.None,

                // Optional visual properties
                Opacity = 0.8,
                Contents = "Direction arrow"
            };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with arrow line annotation to '{outputPath}'.");
    }
}