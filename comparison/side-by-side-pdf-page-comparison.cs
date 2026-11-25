using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPath = "comparison.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the PDFs in using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Options for side‑by‑side visual comparison – use defaults or set desired flags
            var options = new SideBySideComparisonOptions
            {
                // Example: highlight insert/delete changes
                AdditionalChangeMarks = true,
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Compare only the first page of each document using the Page overload
            SideBySidePdfComparer.Compare(doc1.Pages[1], doc2.Pages[1], outputPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{outputPath}'.");
    }
}
