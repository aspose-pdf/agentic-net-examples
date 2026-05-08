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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the separator line will be placed (first page)
            Page page = doc.Pages[1];

            // Define the start and end points of the line (coordinates in points)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(50, 750);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(550, 750);

            // Define a rectangle that encloses the line (required by the constructor)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 740, 550, 760);

            // Create the line annotation using the dedicated constructor
            LineAnnotation line = new LineAnnotation(page, rect, start, end)
            {
                // Set the visual appearance of the line
                Color = Aspose.Pdf.Color.Gray
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}