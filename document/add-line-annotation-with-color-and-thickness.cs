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
        const string outputPath = "output_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure we are working with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Define start and end points for the line (relative to the page)
            Point start = new Point(100, 510);
            Point end   = new Point(300, 510);

            // Create the line annotation on the specified page
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                // Set the line color (use Aspose.Pdf.Color for cross‑platform compatibility)
                Color = Aspose.Pdf.Color.Blue
            };

            // Set line thickness via the Border property (requires the parent annotation in the constructor)
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}