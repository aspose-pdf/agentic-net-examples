using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath   = "watermark_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a list to hold each page's summary line
            var reportLines = new System.Collections.Generic.List<string>();

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Retrieve the page
                Page page = pdfDoc.Pages[pageIndex];

                // Count WatermarkArtifact objects using LINQ
                int watermarkCount = page.Artifacts
                                         .OfType<WatermarkArtifact>()
                                         .Count();

                // Build the summary line for this page
                reportLines.Add($"Page {pageIndex}: {watermarkCount} WatermarkArtifact(s)");
            }

            // Write the summary report to a text file
            File.WriteAllLines(reportPath, reportLines);
        }

        Console.WriteLine($"Watermark artifact summary written to '{reportPath}'.");
    }
}