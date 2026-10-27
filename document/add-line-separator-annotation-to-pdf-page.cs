using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and add a horizontal line as a separator
            foreach (Page page in doc.Pages)
            {
                // Page dimensions
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Y position for the separator (middle of the page)
                double yPos = pageHeight / 2;

                // Create a Graph container that covers the whole page
                Graph graph = new Graph(pageWidth, pageHeight);

                // Define the line (start X, start Y, end X, end Y)
                Line line = new Line(new float[] { 0, (float)yPos, (float)pageWidth, (float)yPos });

                // Visual properties for the line
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Gray,
                    LineWidth = 1,
                    // Opacity can be simulated with a semi‑transparent color if needed
                };

                // Add the line to the graph and the graph to the page
                graph.Shapes.Add(line);
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with line separators to '{outputPath}'.");
    }
}
