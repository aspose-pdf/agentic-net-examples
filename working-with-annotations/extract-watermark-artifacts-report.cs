using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportCsv = "watermark_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Prepare CSV report header
            using (StreamWriter writer = new StreamWriter(reportCsv))
            {
                writer.WriteLine("Page,Left,Bottom,Right,Top,Opacity");

                int totalWatermarks = 0;

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all artifacts on the page
                    foreach (Artifact artifact in page.Artifacts)
                    {
                        // Filter only WatermarkArtifact instances
                        if (artifact is WatermarkArtifact watermark)
                        {
                            totalWatermarks++;

                            // Position is an Aspose.Pdf.Point (X,Y)
                            Aspose.Pdf.Point point = watermark.Position;

                            // Opacity is a double in the range 0..1
                            double opacity = watermark.Opacity;

                            // Write CSV line – using the point for both corners (left/bottom = right/top)
                            writer.WriteLine($"{pageIndex},{point.X},{point.Y},{point.X},{point.Y},{opacity}");
                        }
                    }
                }

                Console.WriteLine($"Watermark report generated: {reportCsv}");
                Console.WriteLine($"Total watermarks found: {totalWatermarks}");
            }
        }
    }
}
