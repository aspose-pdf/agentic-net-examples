using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files (adjust as needed)
        const string inputDir = @"C:\InputPdfs";
        // Output directory for processed PDFs
        const string outputDir = @"C:\OutputPdfs";

        // Validate input directory
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Iterate over all PDF files in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Determine output file path (same file name in the output folder)
            string outputPath = System.IO.Path.Combine(outputDir, System.IO.Path.GetFileName(inputPath));

            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Process each page (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a Graph that covers the whole page
                    Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                    // Define a rectangle shape that will act as the watermark
                    // Parameters: left, bottom, width, height (float values are accepted)
                    Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
                    shapeRect.GraphInfo = new GraphInfo
                    {
                        FillColor = Color.LightGray, // background of the rectangle
                        Color = Color.Black,        // border color
                        LineWidth = 2               // border thickness
                    };

                    // Add the rectangle to the graph
                    graph.Shapes.Add(shapeRect);

                    // Add the graph to the page's content
                    page.Paragraphs.Add(graph);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {inputPath} → {outputPath}");
        }
    }
}
