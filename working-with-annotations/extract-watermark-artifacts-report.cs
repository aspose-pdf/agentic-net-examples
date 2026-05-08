using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // List to hold report lines
        List<string> reportLines = new List<string>();
        reportLines.Add("Watermark Artifacts Report");
        reportLines.Add($"Source PDF: {inputPdfPath}");
        reportLines.Add($"Generated on: {DateTime.Now}");
        reportLines.Add(string.Empty);
        reportLines.Add("Page\tPosition (X,Y)\tOpacity");

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Filter only WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Position may be null if not explicitly set; handle gracefully
                        string positionText = "N/A";
                        if (watermark.Position != null)
                        {
                            // Position is an Aspose.Pdf.Point (X, Y)
                            positionText = $"{watermark.Position.X},{watermark.Position.Y}";
                        }

                        // Opacity is a double in the range 0..1
                        string opacityText = watermark.Opacity.ToString("0.##");

                        reportLines.Add($"{pageIndex}\t{positionText}\t{opacityText}");
                    }
                }
            }
        }

        // Write the report to a text file
        try
        {
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Watermark report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}