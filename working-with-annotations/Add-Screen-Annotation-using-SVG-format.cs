using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "ScreenAnnotation.pdf";
        const string svgMediaPath = "sample.svg";

        // Ensure the SVG media file exists
        if (!File.Exists(svgMediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {svgMediaPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the Screen annotation referencing the SVG file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, svgMediaPath)
            {
                Title = "SVG Media",
                Contents = "Click to view SVG content."
            };

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ScreenAnnotation saved to '{outputPath}'.");
    }
}