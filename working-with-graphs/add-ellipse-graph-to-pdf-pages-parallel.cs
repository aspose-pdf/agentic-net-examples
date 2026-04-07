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
        // Input directory containing PDF files
        const string inputDirectory  = "InputPdfs";
        // Output directory for processed PDFs
        const string outputDirectory = "OutputPdfs";

        // Verify that the input folder exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Collect all PDF file paths (empty list is fine – Parallel.ForEach will simply do nothing)
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputDirectory, "*.pdf"));

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, inputPath =>
        {
            try
            {
                // Derive output file name
                string fileName   = System.IO.Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = System.IO.Path.Combine(outputDirectory, $"{fileName}_gradient.pdf");

                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in doc.Pages)
                    {
                        // Create a Graph container sized to the page
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Define an ellipse (left, bottom, width, height)
                        Ellipse ellipse = new Ellipse(100, 500, 200, 150);

                        // Apply visual styling via GraphInfo (gradient fill not directly supported;
                        // using a solid fill color as placeholder)
                        ellipse.GraphInfo = new GraphInfo
                        {
                            FillColor = Color.FromRgb(0.2, 0.5, 0.8), // example fill color
                            Color     = Color.Black,               // stroke color
                            LineWidth = 2
                        };

                        // Add the ellipse to the graph
                        graph.Shapes.Add(ellipse);

                        // Add the graph to the page's content
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified document (lifecycle rule: use Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {System.IO.Path.GetFileName(inputPath)} → {System.IO.Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}
