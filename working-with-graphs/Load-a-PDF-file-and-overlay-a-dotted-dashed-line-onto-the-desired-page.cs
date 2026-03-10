using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Desired page number (1‑based indexing)
        const int pageNumber = 1;

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the target page
            Page page = doc.Pages[pageNumber];

            // Define the start and end points of the line (PDF coordinate system: origin at bottom‑left)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(100, 500);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(400, 500);

            // Define a rectangle that encloses the line; required by the annotation constructor
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 500);

            // Create a line annotation on the specified page
            LineAnnotation lineAnn = new LineAnnotation(page, rect, start, end);

            // Set the line color
            lineAnn.Color = Aspose.Pdf.Color.Blue;

            // Configure the border (width and dash pattern)
            lineAnn.Border = new Border(lineAnn) { Width = 2 };
            // Dash pattern: 3 units on, 3 units off (dashed). Adjust values for dotted style if needed.
            lineAnn.Border.Dash = new Dash(3, 3);

            // Add the annotation to the page
            page.Annotations.Add(lineAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Dotted/dashed line overlay saved to '{outputPath}'.");
    }
}