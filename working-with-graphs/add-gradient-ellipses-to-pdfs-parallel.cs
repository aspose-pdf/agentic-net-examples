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
        // Input PDF files – adjust the list as needed
        List<string> inputFiles = new List<string>
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        string outputDirectory = "Processed";
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            try
            {
                // Load the PDF (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Add the gradient‑filled ellipses to every page
                    foreach (Page page in doc.Pages)
                    {
                        // Create a Graph container (size can be adjusted). Use double overload to avoid obsolete constructor.
                        Graph graph = new Graph(500.0, 500.0);

                        // Define gradient start and end colors as raw RGB components (Aspose.Pdf.Color does not expose R,G,B properties)
                        double startR = 0.0, startG = 0.0, startB = 1.0; // Blue
                        double endR   = 1.0, endG   = 0.0, endB   = 0.0; // Red

                        int steps = 10; // Number of ellipses to simulate the gradient
                        double width = 400;
                        double height = 400;
                        double centerX = 250;
                        double centerY = 250;
                        double stepSize = Math.Min(width, height) / (2 * steps);

                        for (int i = 0; i < steps; i++)
                        {
                            // Linear interpolation of RGB components
                            double t = (double)i / (steps - 1);
                            double r = startR + t * (endR - startR);
                            double g = startG + t * (endG - startG);
                            double b = startB + t * (endB - startB);
                            Aspose.Pdf.Color fill = Aspose.Pdf.Color.FromRgb(r, g, b);

                            // Create an ellipse that shrinks towards the centre
                            double ellipseWidth  = width  - 2 * i * stepSize;
                            double ellipseHeight = height - 2 * i * stepSize;
                            double left  = centerX - ellipseWidth  / 2;
                            double bottom = centerY - ellipseHeight / 2;

                            Ellipse ellipse = new Ellipse(left, bottom, ellipseWidth, ellipseHeight);
                            ellipse.GraphInfo = new GraphInfo
                            {
                                FillColor = fill,
                                Color = Aspose.Pdf.Color.Black, // optional border color
                                LineWidth = 0.5f
                            };
                            graph.Shapes.Add(ellipse);
                        }

                        // Add the graph to the page
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF (lifecycle rule: use Document.Save)
                    string outputPath = System.IO.Path.Combine(
                        outputDirectory,
                        System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_processed.pdf");
                    doc.Save(outputPath);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}
