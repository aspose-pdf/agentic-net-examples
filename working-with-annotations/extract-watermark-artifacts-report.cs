using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Needed for Point type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "watermark_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create or overwrite the report file
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("Watermark Artifacts Report");
                writer.WriteLine($"Source PDF: {inputPath}");
                writer.WriteLine($"Generated on: {DateTime.Now}");
                writer.WriteLine();

                int pageNumber = 1;
                foreach (Page page in doc.Pages)
                {
                    // Iterate over all artifacts on the current page
                    foreach (Artifact artifact in page.Artifacts)
                    {
                        // Filter only WatermarkArtifact instances
                        if (artifact is WatermarkArtifact watermark)
                        {
                            // Retrieve position (may be null if not explicitly set)
                            var pos = watermark.Position; // Position is a Point
                            string posInfo = pos != null
                                ? $"X={pos.X}, Y={pos.Y}"
                                : "Not set";

                            // Opacity is a value between 0 and 1
                            writer.WriteLine($"Page {pageNumber}: Opacity={watermark.Opacity}, Position={posInfo}");
                        }
                    }
                    pageNumber++;
                }
            }
        }

        Console.WriteLine($"Watermark report saved to '{reportPath}'.");
    }
}
