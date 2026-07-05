using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path (same file name in output folder)
            string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a Graph container sized to the page dimensions
                    double pageWidth  = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;
                    Graph graph = new Graph(pageWidth, pageHeight);

                    // Define the rectangle that will act as the watermark
                    // Position: 100 points from left, 100 points from bottom,
                    // Size: 200x100 points
                    Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 100);

                    // Set visual properties via GraphInfo (FillColor, Border Color, LineWidth)
                    rect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color     = Aspose.Pdf.Color.Black,
                        LineWidth = 2
                    };

                    // Add the rectangle shape to the graph
                    graph.Shapes.Add(rect);

                    // Add the graph to the page's paragraph collection
                    page.Paragraphs.Add(graph);
                }

                // Save the modified document as PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {System.IO.Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}
