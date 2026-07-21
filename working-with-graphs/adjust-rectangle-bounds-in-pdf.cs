using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a Graph container that will hold drawing shapes
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 500.0);

            // Define rectangle parameters that intentionally exceed the page bounds
            float left   = 450f;   // X coordinate (LLX)
            float bottom = 450f;   // Y coordinate (LLY)
            float width  = 100f;   // rectangle width
            float height = 100f;   // rectangle height

            // Drawing.Rectangle expects float parameters (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                left,
                bottom,
                width,
                height);

            // Set visual styling via GraphInfo (LineWidth is a float)
            rect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,          // stroke color
                FillColor = Aspose.Pdf.Color.LightGray,
                LineWidth = 2f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Retrieve page dimensions for bounds checking (convert to float for comparison)
            float pageWidth = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;

            // Determine if the rectangle fits within the page bounds using the stored parameters
            bool fits = left >= 0 &&
                        bottom >= 0 &&
                        left + width <= pageWidth &&
                        bottom + height <= pageHeight;

            if (!fits)
            {
                // Adjust position so the rectangle stays inside the page
                float newLeft   = Math.Max(0f, Math.Min(left,   pageWidth  - width));
                float newBottom = Math.Max(0f, Math.Min(bottom, pageHeight - height));

                // Create a new rectangle with the adjusted coordinates, preserving styling
                Aspose.Pdf.Drawing.Rectangle adjusted = new Aspose.Pdf.Drawing.Rectangle(
                    newLeft,
                    newBottom,
                    width,
                    height);
                adjusted.GraphInfo = rect.GraphInfo; // preserve styling

                // Replace the old rectangle in the graph
                graph.Shapes.Remove(rect);
                graph.Shapes.Add(adjusted);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
