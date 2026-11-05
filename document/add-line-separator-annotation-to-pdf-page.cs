using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_separated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the separator line will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the line geometry: a horizontal line from (50,400) to (550,400)
            // The rectangle defines the annotation's bounding box; for a horizontal line the top and bottom are the same.
            Rectangle lineRect = new Rectangle(50, 400, 550, 400);
            Point startPoint = new Point(50, 400);
            Point endPoint   = new Point(550, 400);

            // Create a line annotation using the constructor that accepts page, rectangle, start and end points
            LineAnnotation line = new LineAnnotation(page, lineRect, startPoint, endPoint)
            {
                Color    = Color.Black, // line color
                Width    = 1,           // line thickness
                Contents = "Section separator" // optional tooltip text
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added. Saved to '{outputPath}'.");
    }
}
