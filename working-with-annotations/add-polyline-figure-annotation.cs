using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_polyline.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices (custom shape)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(180, 560),
                new Aspose.Pdf.Point(240, 540),
                new Aspose.Pdf.Point(300, 580),
                new Aspose.Pdf.Point(360, 540)
            };

            // Create the PolylineAnnotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Set visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Optional: add a popup text
                Contents = "Custom polyline diagram annotation",
                // Optional: set the intent (e.g., dimension)
                Intent = PolyIntent.PolyLineDimension
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}