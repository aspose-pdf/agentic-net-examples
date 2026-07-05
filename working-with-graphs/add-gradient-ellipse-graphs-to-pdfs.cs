using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF files folder
        string inputFolder = "InputPdfs";
        // Output folder for processed PDFs
        string outputFolder = "OutputPdfs";

        // Ensure both folders exist so that Directory.GetFiles does not throw
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        string[] inputFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (inputFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, inputPath =>
        {
            try
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = System.IO.Path.Combine(outputFolder, $"{fileName}_gradient.pdf");

                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Add gradient‑like ellipses to every page
                    foreach (Page page in doc.Pages)
                    {
                        // Create a Graph that covers the whole page
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Parameters for the gradient effect
                        int steps = 10; // number of ellipses
                        double maxRadiusX = page.PageInfo.Width / 4;
                        double maxRadiusY = page.PageInfo.Height / 4;
                        double centerX = page.PageInfo.Width / 2;
                        double centerY = page.PageInfo.Height / 2;

                        for (int i = 0; i < steps; i++)
                        {
                            double factor = (double)i / (steps - 1);

                            // Interpolate fill color from light to dark blue
                            Aspose.Pdf.Color fill = Aspose.Pdf.Color.FromRgb(
                                0.2 + 0.6 * factor, // Red component
                                0.4,                // Green component (constant)
                                0.8 - 0.5 * factor  // Blue component
                            );

                            // Reduce ellipse size for inner steps
                            double radiusX = maxRadiusX * (1 - factor * 0.5);
                            double radiusY = maxRadiusY * (1 - factor * 0.5);

                            // Create ellipse with calculated bounds (cast to float because Ellipse expects float)
                            Ellipse ellipse = new Ellipse(
                                (float)(centerX - radiusX),
                                (float)(centerY - radiusY),
                                (float)(radiusX * 2),
                                (float)(radiusY * 2)
                            );

                            // Set visual properties via GraphInfo
                            ellipse.GraphInfo = new GraphInfo
                            {
                                FillColor = fill,
                                Color = Aspose.Pdf.Color.Black,
                                LineWidth = 0.5f // float literal
                            };

                            graph.Shapes.Add(ellipse);
                        }

                        // Attach the graph to the page
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {inputPath}: {ex.Message}");
            }
        });
    }
}
