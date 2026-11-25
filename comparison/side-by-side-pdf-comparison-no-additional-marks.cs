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

        // Load both PDFs (documents are disposed automatically)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Disable the visual markers that indicate additional changes
                AdditionalChangeMarks = false
                // Other options keep their defaults (e.g., ShowDifferences = true)
            };

            // Perform the comparison and save the result as a PDF
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{outputPath}'.");
    }
}
