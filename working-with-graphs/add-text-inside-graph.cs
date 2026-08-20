using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Graph dimensions and position (use double values as required by the API)
            Graph graph = new Graph(300.0, 200.0);
            graph.Left = 100.0;   // X coordinate of the graph's left side
            graph.Top  = 500.0;   // Y coordinate of the graph's top side

            // Styling for the graph – border and background are set via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                Color = Color.Black,      // border color
                FillColor = Color.White,  // background fill
                LineWidth = 1             // border thickness
            };

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a text fragment to be placed inside the graph
            TextFragment tf = new TextFragment("Hello Aspose.Pdf!");
            // Modify the existing TextState instead of assigning a new one (TextState is read‑only)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.Blue;

            // Position the text (coordinates are relative to the page)
            tf.Position = new Position(150.0, 560.0);

            // Add the text fragment to the page – it will render over the graph
            page.Paragraphs.Add(tf);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
