using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_chart.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define chart size
            double chartWidth = 400;
            double chartHeight = 300;

            // Create a Graph object with the desired size
            Graph graph = new Graph(chartWidth, chartHeight);

            // Align the graph to the centre of the page when added to Paragraphs
            graph.HorizontalAlignment = HorizontalAlignment.Center;
            graph.VerticalAlignment = VerticalAlignment.Center;

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Chart added and saved to '{outputPath}'.");
    }
}
