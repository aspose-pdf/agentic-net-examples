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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            // Note: lower‑left Y must be less than upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

            // Prepare vertices for the polyline annotation
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 480),
                new Aspose.Pdf.Point(200, 420),
                new Aspose.Pdf.Point(280, 460),
                new Aspose.Pdf.Point(360, 380)
            };

            // Create the PolylineAnnotation with the required vertices array
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices);

            // Visual styling
            polyline.Color = Aspose.Pdf.Color.FromRgb(0, 128, 0); // dark green line
            polyline.Border = new Border(polyline) { Width = 2 };

            // Alternative text for accessibility (figure description)
            polyline.Title = "Custom Diagram Figure";
            polyline.Contents = "A polyline illustrating the process flow.";

            // Attach the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation with custom polyline saved to '{outputPath}'.");
    }
}
