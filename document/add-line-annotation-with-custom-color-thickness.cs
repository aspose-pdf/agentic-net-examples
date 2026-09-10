using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that bounds the annotation (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Define start and end points of the line (relative to the page)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(110, 510);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(290, 510);

            // Create the line annotation
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set custom color
            line.Color = Aspose.Pdf.Color.Blue;

            // Set line thickness via Border (requires parent annotation in constructor)
            line.Border = new Border(line) { Width = 3 };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}