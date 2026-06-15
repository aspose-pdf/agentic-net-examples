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
        const string outputPath = "output_with_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page, 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the line annotation
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Define start and end points of the line (relative to page coordinates)
            Point start = new Point(110, 510);
            Point end   = new Point(290, 510);

            // Create the line annotation on the specified page
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set the line color
            line.Color = Aspose.Pdf.Color.Red;

            // Set line thickness via the Border property (requires parent annotation in ctor)
            line.Border = new Border(line) { Width = 2 };

            // Optionally add a tooltip or contents
            line.Contents = "Highlighted section";

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}