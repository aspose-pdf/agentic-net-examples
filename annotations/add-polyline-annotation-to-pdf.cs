using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PolylinePath.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the annotation
            Page page = doc.Pages[1];

            // Define a rectangle that encloses the polyline points (annotation bounds)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(40, 40, 160, 160);

            // Define the polyline vertices
            Point[] vertices = new Point[]
            {
                new Point(50, 50),
                new Point(100, 150),
                new Point(150, 50)
            };

            // Create the polyline annotation with required properties
            PolylineAnnotation poly = new PolylineAnnotation(page, rect, vertices)
            {
                Subject = "Path",
                Opacity = 0.8f
            };

            // Attach the annotation to the page
            page.Annotations.Add(poly);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved annotated PDF to '{outputPath}'.");
    }
}
