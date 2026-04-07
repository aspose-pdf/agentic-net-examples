using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class TranslateGraphElement
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (acts like a paragraph)
            // Graph constructor expects double values
            Graph graph = new Graph(400.0, 200.0);

            // Initial rectangle parameters
            float rectX = 100f;   // lower‑left X
            float rectY = 500f;   // lower‑left Y
            float rectWidth = 200f;
            float rectHeight = 100f;

            // Define a rectangle shape at the initial position
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            // Add the rectangle shape to the graph
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Translate the rectangle shape horizontally (dx) and vertically (dy)
            float dx = 50f; // shift right by 50 points
            float dy = 30f; // shift up by 30 points

            // Re‑create the rectangle with the translated coordinates and replace the old one
            float newX = rectX + dx;
            float newY = rectY + dy;
            var translatedRect = new Aspose.Pdf.Drawing.Rectangle(newX, newY, rectWidth, rectHeight);
            translatedRect.GraphInfo = shapeRect.GraphInfo; // preserve styling

            // Replace the original shape in the graph
            graph.Shapes.Remove(shapeRect);
            graph.Shapes.Add(translatedRect);

            // Save the resulting PDF
            doc.Save("translated_graph.pdf");
        }

        Console.WriteLine("PDF with translated graph element saved as 'translated_graph.pdf'.");
    }
}
