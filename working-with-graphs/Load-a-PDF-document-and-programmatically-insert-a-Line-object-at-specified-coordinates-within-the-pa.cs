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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the line will be added (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle that bounds the line
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 500);

            // Define start and end points of the line (user space coordinates)
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(100, 500);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(300, 500);

            // Create the line annotation and set its visual properties
            LineAnnotation lineAnn = new LineAnnotation(page, rect, start, end)
            {
                Color = Aspose.Pdf.Color.Red, // line color
                Width = 2                      // line thickness
            };

            // Add the annotation to the page
            page.Annotations.Add(lineAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line inserted and saved to '{outputPath}'.");
    }
}