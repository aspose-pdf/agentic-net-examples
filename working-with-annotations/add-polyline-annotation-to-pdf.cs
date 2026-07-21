using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_polyline.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Define the polyline vertices (page coordinates)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(200, 600),
                new Aspose.Pdf.Point(300, 550),
                new Aspose.Pdf.Point(380, 680)
            };

            // Create the polyline annotation with custom appearance
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Color = Aspose.Pdf.Color.Blue,          // line color
                Contents = "Custom diagram polyline",   // tooltip text
                Intent = PolyIntent.Undefined           // default intent
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}