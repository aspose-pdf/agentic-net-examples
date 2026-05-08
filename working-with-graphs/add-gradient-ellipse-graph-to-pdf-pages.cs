using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF files (could be populated dynamically)
        List<string> inputFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Output directory for processed PDFs
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
                return;

            // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = System.IO.Path.Combine(outputDir, $"{fileName}_gradient.pdf");

            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a Graph that covers the whole page
                    Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                    // Define number of ellipses to simulate a gradient
                    int ellipseCount = 10;
                    double step = page.PageInfo.Width / ellipseCount;

                    // Add ellipses with gradually changing fill colors
                    for (int j = 0; j < ellipseCount; j++)
                    {
                        double left = j * step;
                        double bottom = 0;
                        double width = step;
                        double height = page.PageInfo.Height;

                        Ellipse ellipse = new Ellipse(left, bottom, width, height);

                        // Compute a color that transitions from light blue to dark blue
                        int blue = 255 - (j * 20);
                        if (blue < 0) blue = 0;

                        ellipse.GraphInfo = new GraphInfo
                        {
                            FillColor = Color.FromRgb(0, 0, blue),
                            Color = Color.Black,
                            LineWidth = 0.5f
                        };

                        graph.Shapes.Add(ellipse);
                    }

                    // Add the graph to the page
                    page.Paragraphs.Add(graph);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }
        });
    }
}
