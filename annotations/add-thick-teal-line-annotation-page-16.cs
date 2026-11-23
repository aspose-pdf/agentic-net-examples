using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ThickTealLine.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 16 pages
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("The document does not contain page 16.");
                return;
            }

            // Get page 16 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[16];

            // Define the annotation rectangle (left, bottom, right, top)
            Rectangle rect = new Rectangle(100, 500, 400, 500);

            // Create the line annotation with start and end points
            LineAnnotation line = new LineAnnotation(page, rect,
                new Point(100, 500),   // start point
                new Point(400, 500))   // end point
            {
                // Set line color to teal (RGB 0,128,128) – values are 0‑1 range
                Color = Color.FromRgb(0, 0.5, 0.5)
            };

            // Set line width via the Border property (Width is in points)
            line.Border = new Border(line) { Width = 5 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with thick teal line to '{outputPath}'.");
    }
}
