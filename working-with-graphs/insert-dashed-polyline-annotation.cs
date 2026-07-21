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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(200, 580),
                new Aspose.Pdf.Point(300, 540),
                new Aspose.Pdf.Point(380, 600)
            };

            // Create the polyline annotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Color = Aspose.Pdf.Color.Blue,   // Stroke color
                Contents = "Sample polyline"
            };

            // Apply a dashed border style
            polyline.Border = new Border(polyline)
            {
                Width = 2,                       // Border thickness
                Dash = new Dash(5, 3)            // 5 units dash, 3 units gap
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation saved to '{outputPath}'.");
    }
}