using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string reportPath = "watermark_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text report file
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("Watermark Artifacts Report");
                writer.WriteLine($"Document: {Path.GetFileName(inputPath)}");
                writer.WriteLine();

                int pageNumber = 1;
                // Iterate through all pages (1‑based indexing)
                foreach (Page page in doc.Pages)
                {
                    // Iterate through artifacts on the current page
                    foreach (Artifact artifact in page.Artifacts)
                    {
                        // Identify WatermarkArtifact instances
                        if (artifact is WatermarkArtifact watermark)
                        {
                            // Position can be a Rectangle or other struct; ToString provides readable output
                            var position = watermark.Position;
                            // Opacity is a double in the range 0..1
                            double opacity = watermark.Opacity;

                            writer.WriteLine($"Page {pageNumber}:");
                            writer.WriteLine($"  Position: {position}");
                            writer.WriteLine($"  Opacity : {opacity}");
                        }
                    }
                    pageNumber++;
                }
            }
        }

        Console.WriteLine($"Watermark report generated at '{reportPath}'.");
    }
}