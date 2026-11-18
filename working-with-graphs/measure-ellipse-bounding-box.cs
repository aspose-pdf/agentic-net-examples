using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Define ellipse bounds (lower‑left x/y and upper‑right x/y)
            double llx = 100, lly = 500, urx = 300, ury = 700;

            // Create a Graph container – all drawing shapes must be added to a Graph
            // Width = urx‑llx, Height = ury‑lly
            Graph graph = new Graph((float)(urx - llx), (float)(ury - lly));
            graph.Left = (float)llx;   // X position of the graph on the page
            graph.Top = (float)lly;    // Y position of the graph on the page (Bottom‑left origin)

            // Create the ellipse shape with the defined bounds relative to the graph
            Ellipse ellipse = new Ellipse(0, 0, (float)(urx - llx), (float)(ury - lly));

            // Apply a solid fill (gradient fill can be simulated via PatternColorSpace if needed)
            ellipse.GraphInfo.FillColor = Color.LightBlue;
            ellipse.GraphInfo.Color = Color.Black; // Stroke color
            ellipse.GraphInfo.LineWidth = 1;

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Retrieve the bounding box – the geometric bounds are the same as the original coordinates
            // (the fill style does not alter the geometric bounds)
            Aspose.Pdf.Rectangle bbox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Output the bounding box coordinates and dimensions
            Console.WriteLine("Ellipse Bounding Box:");
            Console.WriteLine($"LLX: {bbox.LLX}, LLY: {bbox.LLY}");
            Console.WriteLine($"URX: {bbox.URX}, URY: {bbox.URY}");
            Console.WriteLine($"Width: {bbox.Width}, Height: {bbox.Height}");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
