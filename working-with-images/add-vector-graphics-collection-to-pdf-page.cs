using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF (can be empty)
        const string outputPdf = "output.pdf"; // result PDF with added graphics

        // Load existing document or create a new empty one
        Document doc;
        if (File.Exists(inputPdf))
        {
            doc = new Document(inputPdf);
        }
        else
        {
            doc = new Document();
            // Ensure there is at least one page to work with
            doc.Pages.Add();
        }

        // Add a new blank page at the end of the document
        Page newPage = doc.Pages.Add();

        // -----------------------------------------------------------------
        // Prepare a collection of vector graphics (shapes) to be added.
        // -----------------------------------------------------------------
        List<Shape> vectorGraphics = new List<Shape>();

        // Example: a red line
        float[] linePoints = { 100f, 500f, 300f, 500f };
        Line line = new Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Color.Red,
            LineWidth = 2f
        };
        vectorGraphics.Add(line);

        // Example: a blue rectangle (use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
        Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(150f, 400f, 200f, 100f);
        rectShape.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightBlue,
            Color = Color.Blue,
            LineWidth = 1f
        };
        vectorGraphics.Add(rectShape);

        // Example: a green ellipse
        Ellipse ellipse = new Ellipse(200f, 300f, 150f, 100f);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Color.LightGreen,
            Color = Color.Green,
            LineWidth = 1.5f
        };
        vectorGraphics.Add(ellipse);

        // -----------------------------------------------------------------
        // Add the collection of shapes to the new page.
        // -----------------------------------------------------------------
        // Use the double‑based Graph constructor (the float overload is obsolete).
        Graph graph = new Graph(500.0, 800.0); // width, height of the drawing canvas

        foreach (Shape shape in vectorGraphics)
        {
            graph.Shapes.Add(shape);
        }

        // Attach the graph to the page
        newPage.Paragraphs.Add(graph);

        // Save the modified document
        doc.Save(outputPdf);
        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
