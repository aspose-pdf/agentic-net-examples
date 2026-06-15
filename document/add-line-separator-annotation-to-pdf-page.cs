using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the separator will be added (first page in this example)
            Page page = doc.Pages[1];

            // Define the start and end points of the line (horizontal line across the page)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(50, 500);   // left side
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(550, 500); // right side

            // Define a rectangle that bounds the line annotation (small height around the line)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 495, 550, 505);

            // Create the line annotation with the specified page, rectangle, and points
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                // Set the line color (gray separator)
                Color = Aspose.Pdf.Color.Gray
            };

            // Set the line width via the Border property (requires the parent annotation in the constructor)
            line.Border = new Border(line) { Width = 1 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line separator added and saved to '{outputPath}'.");
    }
}