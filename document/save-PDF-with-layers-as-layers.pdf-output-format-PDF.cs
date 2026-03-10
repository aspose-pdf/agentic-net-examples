using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "layers.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Initialize the Layers collection for the page
            page.Layers = new List<Layer>();

            // -------------------------------------------------
            // Create first layer with a name and an optional content group ID
            // -------------------------------------------------
            Layer layer1 = new Layer("Layer 1", "OCG1");

            // Add drawing operators to the layer (a red horizontal line)
            layer1.Contents.Add(new SetRGBColorStroke(1.0, 0.0, 0.0)); // Red stroke color
            layer1.Contents.Add(new MoveTo(100, 500));               // Move to start point
            layer1.Contents.Add(new LineTo(300, 500));               // Draw line to end point
            layer1.Contents.Add(new Stroke());                       // Stroke the path

            // Attach the layer to the page
            page.Layers.Add(layer1);

            // -------------------------------------------------
            // Create second layer with a different color (green) and draw a rectangle
            // -------------------------------------------------
            Layer layer2 = new Layer("Layer 2", "OCG2");

            // Set stroke color to green
            layer2.Contents.Add(new SetRGBColorStroke(0.0, 1.0, 0.0)); // Green stroke color
            // Define rectangle corners
            layer2.Contents.Add(new MoveTo(150, 600));
            layer2.Contents.Add(new LineTo(350, 600));
            layer2.Contents.Add(new LineTo(350, 800));
            layer2.Contents.Add(new LineTo(150, 800));
            layer2.Contents.Add(new LineTo(150, 600));
            layer2.Contents.Add(new Stroke());

            // Attach the second layer to the page
            page.Layers.Add(layer2);

            // -------------------------------------------------
            // Save the document as a PDF containing the defined layers
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layers saved to '{outputPath}'.");
    }
}