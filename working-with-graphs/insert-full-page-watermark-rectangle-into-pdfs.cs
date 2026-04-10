using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = @"C:\InputPdfs";
        // Output directory where modified PDFs will be saved
        const string outputDir = @"C:\OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Iterate over all PDF files in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = System.IO.Path.GetFileName(inputPath);
            string outputPath = System.IO.Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document (using statement ensures proper disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Process each page (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Create a Graph that covers the whole page
                        double pageWidth = page.PageInfo.Width;
                        double pageHeight = page.PageInfo.Height;
                        var graph = new Aspose.Pdf.Drawing.Graph(pageWidth, pageHeight);

                        // Define a rectangle shape that acts as a watermark
                        var watermarkRect = new Aspose.Pdf.Drawing.Rectangle(
                            0f,                     // left
                            0f,                     // bottom
                            (float)pageWidth,       // width
                            (float)pageHeight);     // height

                        // Set visual properties via GraphInfo
                        watermarkRect.GraphInfo = new Aspose.Pdf.GraphInfo
                        {
                            FillColor = Aspose.Pdf.Color.FromRgb(0.9f, 0.9f, 0.9f), // light gray fill
                            Color = Aspose.Pdf.Color.DarkGray,                  // border color
                            LineWidth = 1f                                      // border thickness
                        };

                        // Add the rectangle to the graph
                        graph.Shapes.Add(watermarkRect);

                        // Add the graph to the page's content
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified document to the output path
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
