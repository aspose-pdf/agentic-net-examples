using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "InkMultipleStrokes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 18 pages (1‑based indexing)
            if (doc.Pages.Count < 18)
            {
                Console.Error.WriteLine("Document does not contain page 18.");
                return;
            }

            // Get page 18
            Page page = doc.Pages[18];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Prepare three separate strokes (each stroke is an array of points)
            Point[] stroke1 = new Point[]
            {
                new Point(120, 520),
                new Point(150, 560),
                new Point(180, 540)
            };

            Point[] stroke2 = new Point[]
            {
                new Point(200, 600),
                new Point(250, 650),
                new Point(300, 620)
            };

            Point[] stroke3 = new Point[]
            {
                new Point(320, 700),
                new Point(350, 730),
                new Point(380, 710)
            };

            // InkList expects IList<Point[]> where each entry represents a stroke
            IList<Point[]> inkList = new List<Point[]> { stroke1, stroke2, stroke3 };

            // Create the InkAnnotation using the Document constructor (the overload that accepts an InkList)
            InkAnnotation ink = new InkAnnotation(doc, inkList);
            ink.Rect = rect; // assign the rectangle after construction

            // Set border width to 1 (Border constructor requires the parent annotation)
            ink.Border = new Border(ink) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(ink);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ink annotation saved to '{outputPath}'.");
    }
}
