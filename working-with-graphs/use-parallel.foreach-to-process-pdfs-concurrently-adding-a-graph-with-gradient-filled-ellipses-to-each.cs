using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Resolve input / output folders in a platform‑independent way.
        // If the absolute Windows style paths do not exist (e.g., on Linux/macOS), fall back to folders
        // relative to the current working directory.
        string inputFolder = ResolveFolderPath(@"C:\InputPdfs", "InputPdfs");
        string outputFolder = ResolveFolderPath(@"C:\OutputPdfs", "OutputPdfs");

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder actually exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF file paths (1‑based indexing is handled later by Aspose)
        List<string> pdfFiles = Directory.GetFiles(inputFolder, "*.pdf").ToList();

        if (!pdfFiles.Any())
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Create a Graph container that covers the whole page
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Gradient definition (blue → red)
                        double startR = 0.0, startG = 0.0, startB = 1.0; // Blue
                        double endR   = 1.0, endG   = 0.0, endB   = 0.0; // Red

                        int steps = 10; // Number of ellipses to simulate the gradient
                        double ellipseWidth  = 200;
                        double ellipseHeight = 200;
                        double centerX = page.PageInfo.Width / 2;
                        double centerY = page.PageInfo.Height / 2;

                        for (int i = 0; i < steps; i++)
                        {
                            // Interpolate colour components
                            double t = (double)i / (steps - 1);
                            double r = startR + t * (endR - startR);
                            double g = startG + t * (endG - startG);
                            double b = startB + t * (endB - startB);
                            Aspose.Pdf.Color fill = Aspose.Pdf.Color.FromRgb(r, g, b);

                            // Shrink each successive ellipse to create a gradient effect
                            double offset = i * 10; // increase offset for inner ellipses
                            Ellipse ellipse = new Ellipse(
                                centerX - ellipseWidth / 2 + offset,
                                centerY - ellipseHeight / 2 + offset,
                                ellipseWidth - 2 * offset,
                                ellipseHeight - 2 * offset);

                            // Set visual properties via GraphInfo
                            ellipse.GraphInfo = new GraphInfo
                            {
                                FillColor = fill,
                                Color = Aspose.Pdf.Color.Black, // stroke colour
                                LineWidth = 1
                            };

                            // Add ellipse to the graph
                            graph.Shapes.Add(ellipse);
                        }

                        // Add the graph to the page
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF to the output folder
                    string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {System.IO.Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Returns a folder path that works on the current OS.
    /// If the absolute Windows‑style path exists, it is used.
    /// Otherwise a folder relative to the current working directory is created/used.
    /// </summary>
    private static string ResolveFolderPath(string windowsStylePath, string fallbackFolderName)
    {
        if (Directory.Exists(windowsStylePath))
            return windowsStylePath;

        // Fallback to a folder next to the executable (or current directory)
        string relativePath = System.IO.Path.Combine(Environment.CurrentDirectory, fallbackFolderName);
        Directory.CreateDirectory(relativePath);
        return relativePath;
    }
}
