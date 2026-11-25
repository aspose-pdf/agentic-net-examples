using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure side‑by‑side comparison options
            var options = new SideBySideComparisonOptions
            {
                // Example: highlight insert/delete changes
                AdditionalChangeMarks = true
            };

            // Define rectangles to exclude from the second document (ExcludeAreas2)
            // Rectangle coordinates are in points: lower‑left X/Y, upper‑right X/Y
            var excludeAreas2 = new List<Aspose.Pdf.Rectangle>
            {
                new Aspose.Pdf.Rectangle(50, 700, 550, 750),   // Example header area
                new Aspose.Pdf.Rectangle(100, 100, 500, 150)   // Example footer area
            };
            // ExcludeAreas2 expects an array, not a List
            options.ExcludeAreas2 = excludeAreas2.ToArray();

            // Perform the side‑by‑side comparison and save the result PDF
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
