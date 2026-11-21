using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // for Point and Rectangle
using Aspose.Pdf;        // for Color (fully qualified when needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "diagram.pdf";
        const string outputPath = "diagram_with_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define start and end points of the line (coordinates are in points; origin is bottom‑left)
            Point start = new Point(100, 500);
            Point end   = new Point(300, 500);

            // Create the line annotation using the constructor that takes a Page, a dummy Rectangle,
            // and the start/end points.
            // The Rectangle argument is required by the API but its values are ignored for line annotations.
            LineAnnotation line = new LineAnnotation(page, new Rectangle(0, 0, 0, 0), start, end);

            // Set line appearance
            line.StartingStyle = LineEnding.None;          // no arrow at the start
            line.EndingStyle   = LineEnding.OpenArrow;    // open arrow at the end
            line.InteriorColor = Aspose.Pdf.Color.FromRgb(0, 0, 1); // blue line

            // NOTE: The Aspose.Pdf API version referenced in the provided rules does not expose a
            // property for custom dash patterns on LineAnnotation. If a dash pattern is required,
            // it would normally be set via a property such as LineDashPattern, which is not listed
            // in the documented members. Therefore the annotation is added without a dash pattern.

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}