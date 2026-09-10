using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output directories
        const string inputDir = "InputPdfs";
        const string outputDir = "OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Collect all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, pdfPath =>
        {
            try
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = System.IO.Path.Combine(outputDir, $"{fileName}_modified.pdf");

                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through each page and add a graph with ellipses
                    foreach (Page page in doc.Pages)
                    {
                        // Create a Graph container sized to the page
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Parameters for gradient‑like ellipses
                        int ellipseCount = 5;
                        double centerX = page.PageInfo.Width / 2;
                        double centerY = page.PageInfo.Height / 2;
                        double maxRadiusX = page.PageInfo.Width / 4;
                        double maxRadiusY = page.PageInfo.Height / 4;

                        for (int i = 0; i < ellipseCount; i++)
                        {
                            double factor = (double)i / (ellipseCount - 1);
                            double radiusX = maxRadiusX * (1 - factor * 0.5);
                            double radiusY = maxRadiusY * (1 - factor * 0.5);
                            double left = centerX - radiusX;
                            double bottom = centerY - radiusY;
                            double width = radiusX * 2;
                            double height = radiusY * 2;

                            // Create an ellipse shape
                            Ellipse ellipse = new Ellipse(left, bottom, width, height);

                            // Simulate a gradient by varying the fill color
                            ellipse.GraphInfo = new GraphInfo
                            {
                                FillColor = Aspose.Pdf.Color.FromRgb(0.2 + 0.6 * factor,
                                                                    0.4 + 0.4 * factor,
                                                                    1.0 - 0.5 * factor),
                                Color = Aspose.Pdf.Color.Black,
                                LineWidth = 0.5f // float literal required
                            };

                            graph.Shapes.Add(ellipse);
                        }

                        // Add the graph to the page
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF (lifecycle rule: use Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });
    }
}
