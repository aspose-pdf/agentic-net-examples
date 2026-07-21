using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where processed PDFs will be saved
        const string outputDirectory = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Gather all PDF file paths
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputDirectory, "*.pdf"));

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Create a Graph container matching the page size
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Simulate a vertical gradient by drawing multiple ellipses
                        // with progressively changing fill colors.
                        int ellipseCount = 10;
                        double ellipseWidth = page.PageInfo.Width * 0.6;
                        double ellipseHeight = page.PageInfo.Height * 0.6;
                        double left = (page.PageInfo.Width - ellipseWidth) / 2;
                        double bottom = (page.PageInfo.Height - ellipseHeight) / 2;

                        for (int i = 0; i < ellipseCount; i++)
                        {
                            // Calculate a color that transitions from light blue to dark blue
                            double t = (double)i / (ellipseCount - 1); // 0..1
                            // Light blue (RGB 173,216,230) to dark blue (RGB 0,0,139)
                            int r = (int)(173 * (1 - t));
                            int g = (int)(216 * (1 - t));
                            int b = (int)(230 * (1 - t) + 139 * t);
                            Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);

                            // Create an ellipse shape
                            Ellipse ellipse = new Ellipse(left, bottom, ellipseWidth, ellipseHeight);
                            ellipse.GraphInfo = new GraphInfo
                            {
                                FillColor = fillColor,
                                Color = Aspose.Pdf.Color.Black, // stroke color
                                LineWidth = 1
                            };

                            // Add the ellipse to the graph
                            graph.Shapes.Add(ellipse);
                        }

                        // Add the graph to the page's paragraphs collection
                        page.Paragraphs.Add(graph);
                    }

                    // Determine output file path
                    string fileName = System.IO.Path.GetFileName(pdfPath);
                    string outputPath = System.IO.Path.Combine(outputDirectory, fileName);

                    // Save the modified PDF (lifecycle rule: save inside using block)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}
