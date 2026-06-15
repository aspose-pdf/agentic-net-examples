using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document (lifecycle: using ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Define the polyline vertices
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(150, 560),
                new Aspose.Pdf.Point(200, 540),
                new Aspose.Pdf.Point(250, 580)
            };

            // Create the polyline annotation on the page
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices);

            // Set the annotation color
            polyline.Color = Aspose.Pdf.Color.Blue;

            // Apply a dashed border style (dash length = 3, gap = 2)
            polyline.Border = new Border(polyline) { Dash = new Dash(3, 2) };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified PDF (lifecycle: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}