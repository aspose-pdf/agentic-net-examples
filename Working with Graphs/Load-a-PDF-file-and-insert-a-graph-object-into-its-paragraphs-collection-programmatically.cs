using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                // Insert a blank page at position 1 if the document is empty
                doc.Pages.Insert(1);
            }

            // Create a Graph object (width, height) measured in points – use double literals as required by the API
            Graph graph = new Graph(400.0, 200.0);
            // Title expects a TextFragment, not a raw string
            graph.Title = new TextFragment("Sample Graph");

            // OPTIONAL: add a simple rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Insert the graph into the first page's paragraphs collection
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            firstPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph inserted and saved to '{outputPath}'.");
    }
}
