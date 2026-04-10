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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the separator will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define a thin rectangle that spans the page width.
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                0,                     // left
                0,                     // bottom
                page.PageInfo.Width,   // right (page width)
                1);                    // top (height of 1 point)

            // Define start and end points for the horizontal line.
            // Place the line at Y = 500 points from the bottom of the page.
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(0, 500);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(page.PageInfo.Width, 500);

            // Create the line annotation and set its visual properties.
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                Color = Aspose.Pdf.Color.Gray   // line color
            };

            // Add the annotation to the page.
            page.Annotations.Add(line);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator line added and saved to '{outputPath}'.");
    }
}