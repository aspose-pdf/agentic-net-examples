using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Create a new PDF document and ensure deterministic disposal
using (Document doc = new Document())
{
    // Add a blank page (1‑based indexing)
    Page page = doc.Pages.Add();

    // Create a Graph container (acts like a paragraph that can hold vector shapes)
    // Use the double‑based constructor as required by the API
    Graph graph = new Graph(400.0, 200.0);

    // Define a rectangle shape (left, bottom, width, height) – use the drawing rectangle type
    var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 100f, 50f);

    // Set visual appearance via GraphInfo (FillColor, Stroke Color, LineWidth)
    rectShape.GraphInfo = new GraphInfo
    {
        FillColor = Color.LightGray,
        Color     = Color.Black,
        LineWidth = 2f
    };

    // Add the rectangle to the graph
    graph.Shapes.Add(rectShape);

    // Add the graph to the page's paragraph collection
    page.Paragraphs.Add(graph);

    // ---- Translate (move) the rectangle horizontally and vertically ----
    // Desired offsets (points)
    double dx = 100; // move right by 100 points
    double dy =  50; // move up by 50 points

    // Adjust the rectangle's position by modifying Left and Bottom
    rectShape.Left   += (float)dx;
    rectShape.Bottom += (float)dy;

    // Save the resulting PDF
    doc.Save("translated_graph.pdf");
}