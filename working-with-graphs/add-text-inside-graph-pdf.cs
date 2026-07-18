using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph (graphics container) with desired width and height
            // Width = 200 points, Height = 100 points
            Graph graph = new Graph(200, 100)
            {
                // Position the graph on the page (left, top)
                Left = 100,   // X coordinate
                Top  = 500    // Y coordinate (from bottom of the page)
            };

            // Add a rectangle shape to the graph as a background
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment text = new TextFragment("Sample Text");

            // Position the text inside the graph (relative to page coordinates)
            // Adjust X/Y to place the text where needed
            text.Position = new Position(120, 540); // X = graph.Left + 20, Y = graph.Top - 40

            // Set font family, size, and color using TextState
            text.TextState.Font = FontRepository.FindFont("Helvetica");
            text.TextState.FontSize = 14;
            text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the page (it will appear over the graph)
            page.Paragraphs.Add(text);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with text inside a graph has been created.");
    }
}