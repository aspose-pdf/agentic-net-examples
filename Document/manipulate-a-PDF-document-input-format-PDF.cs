using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation on the first page
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, annotRect)
            {
                Title = "Note",
                Contents = "This is a sample text annotation.",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };
            page.Annotations.Add(textAnn);

            // Draw a rectangle shape using Graph
            Graph graph = new Graph(400, 200); // container size
            // Rectangle shape: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(shapeRect);
            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Set alternative text for all images on all pages
            const string altText = "Descriptive alternative text for image.";
            foreach (Page pg in doc.Pages)
            {
                foreach (XImage img in pg.Resources.Images)
                {
                    // The method requires the page reference
                    img.TrySetAlternativeText(altText, pg);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document processed and saved to '{outputPath}'.");
    }
}