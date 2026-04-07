using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

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

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Watermark Artifacts Report");
        sb.AppendLine($"Generated: {DateTime.Now}");
        sb.AppendLine();

        // Load PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all artifacts on the page
                foreach (Artifact art in page.Artifacts)
                {
                    // Identify WatermarkArtifact instances
                    if (art is WatermarkArtifact watermark)
                    {
                        // WatermarkArtifact provides a Rectangle property; Position is a Point and cannot be used as a fallback.
                        Aspose.Pdf.Rectangle pos = watermark.Rectangle;
                        double opacity = watermark.Opacity;

                        sb.AppendLine($"Page {i}:");
                        if (pos != null)
                        {
                            sb.AppendLine($"  Position: LLX={pos.LLX}, LLY={pos.LLY}, URX={pos.URX}, URY={pos.URY}");
                        }
                        else
                        {
                            sb.AppendLine("  Position: (not set)");
                        }
                        sb.AppendLine($"  Opacity: {opacity}");
                    }
                }
            }
        }

        // Write the report to a text file
        File.WriteAllText(reportPath, sb.ToString());
        Console.WriteLine($"Report saved to '{reportPath}'.");
    }
}
