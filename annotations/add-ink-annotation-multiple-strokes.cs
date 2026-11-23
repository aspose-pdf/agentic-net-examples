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
        const string outputPath = "InkStrokes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (full page size in this example)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

            // Create the InkAnnotation with an empty InkList
            InkAnnotation ink = new InkAnnotation(doc, new List<Point[]>());
            ink.Rect = rect;

            // Set border width to 2. The Border class does not expose a CapStyle property in the current API,
            // so only the width (and optionally style) can be configured.
            ink.Border = new Border(ink)
            {
                Width = 2,
                Style = BorderStyle.Solid
            };

            // First stroke: a simple line
            Point[] stroke1 = new Point[]
            {
                new Point(100, 700),
                new Point(200, 750),
                new Point(300, 720)
            };

            // Second stroke: a V‑shaped line
            Point[] stroke2 = new Point[]
            {
                new Point(150, 600),
                new Point(250, 650),
                new Point(350, 600)
            };

            // Add the strokes to the annotation
            ink.InkList.Add(stroke1);
            ink.InkList.Add(stroke2);

            // Attach the annotation to the page
            page.Annotations.Add(ink);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ink annotation added and saved to '{outputPath}'.");
    }
}
