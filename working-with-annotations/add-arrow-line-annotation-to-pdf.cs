using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a line annotation with start and end points
            LineAnnotation line = new LineAnnotation(
                page,
                rect,
                new Aspose.Pdf.Point(150, 520), // start point
                new Aspose.Pdf.Point(250, 520)  // end point
            );

            // Set visual appearance
            line.Color = Aspose.Pdf.Color.Blue;

            // No special start style, arrow at the end
            line.StartingStyle = Aspose.Pdf.Annotations.LineEnding.None;
            line.EndingStyle   = Aspose.Pdf.Annotations.LineEnding.ClosedArrow; // arrow head

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with arrow line annotation to '{outputPath}'.");
    }
}