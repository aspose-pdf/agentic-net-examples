using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath   = "watermark_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            writer.WriteLine("Watermark Artifacts Report");
            writer.WriteLine($"Generated on {DateTime.Now}");
            writer.WriteLine(new string('=', 40));

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over artifacts on the current page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Filter only WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Position is an Aspose.Pdf.Point (X,Y)
                        Aspose.Pdf.Point pos = watermark.Position;
                        double opacity = watermark.Opacity;

                        // Write details to the report
                        writer.WriteLine($"Page {pageIndex}:");
                        writer.WriteLine($"  Position => X:{pos.X}, Y:{pos.Y}");
                        writer.WriteLine($"  Opacity  => {opacity}");
                        writer.WriteLine();
                    }
                }
            }

            writer.WriteLine("End of Report");
        }

        Console.WriteLine($"Watermark report generated at '{reportPath}'.");
    }
}
