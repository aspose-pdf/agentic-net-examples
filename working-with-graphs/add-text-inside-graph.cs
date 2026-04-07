using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size: width=400, height=200)
            // NOTE: use double literals as the constructor now expects double values
            Graph graph = new Graph(400.0, 200.0);
            // Optional visual styling for the graph (light gray border)
            graph.GraphInfo = new GraphInfo
            {
                Color = Color.LightGray,
                LineWidth = 1f // float literal
            };
            // Position the graph on the page (left=100, top=500)
            graph.Left = 100.0;
            graph.Top = 500.0;

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello Graph");
            // Modify the existing TextState instance (do NOT assign a new TextState)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Color.Blue;
            // Position the text at specific coordinates inside the graph
            tf.Position = new Position(150, 550); // X = 150, Y = 550 (points)

            // Add the text fragment to the page (it will appear over the graph)
            page.Paragraphs.Add(tf);

            // Save the PDF (lifecycle: save)
            doc.Save("output.pdf");
        }
    }
}
