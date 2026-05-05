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
            // Select the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Define start and end points of the line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(100, 510);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(300, 510);

            // Create the line annotation on the specified page
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set a custom color for the line
            line.Color = Aspose.Pdf.Color.Red;

            // Set line thickness via the Border property (requires the annotation instance)
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
