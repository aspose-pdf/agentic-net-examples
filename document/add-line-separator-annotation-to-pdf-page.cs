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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the separator will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define start and end points of the line (coordinates are in points)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(50, 750);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(550, 750);

            // Define a rectangle that encloses the line (required by the constructor)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 750);

            // Create the line annotation
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                Color = Aspose.Pdf.Color.Gray
            };

            // Set line width via Border (requires the parent annotation in the constructor)
            line.Border = new Border(line) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}