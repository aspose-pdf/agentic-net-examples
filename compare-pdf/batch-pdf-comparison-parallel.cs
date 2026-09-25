using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfBatchComparer
{
    // Compare two PDFs: page count and full text content
    private static bool ComparePdf(string referencePath, string targetPath)
    {
        // Ensure both files exist
        if (!File.Exists(referencePath) || !File.Exists(targetPath))
            return false;

        // Load reference PDF
        using (Document refDoc = new Document(referencePath))
        // Load target PDF
        using (Document tgtDoc = new Document(targetPath))
        {
            // Compare page counts (1‑based indexing)
            if (refDoc.Pages.Count != tgtDoc.Pages.Count)
                return false;

            // Extract full text from reference PDF
            TextAbsorber refAbsorber = new TextAbsorber();
            refDoc.Pages.Accept(refAbsorber);
            string refText = refAbsorber.Text;

            // Extract full text from target PDF
            TextAbsorber tgtAbsorber = new TextAbsorber();
            tgtDoc.Pages.Accept(tgtAbsorber);
            string tgtText = tgtAbsorber.Text;

            // Compare text content (exact match)
            return string.Equals(refText, tgtText, StringComparison.Ordinal);
        }
    }

    static void Main()
    {
        // Path to the reference PDF against which all others are compared
        const string referencePdfPath = "reference.pdf";

        // Directory containing PDFs to compare
        const string pdfDirectory = "PdfBatch";

        if (!File.Exists(referencePdfPath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePdfPath}");
            return;
        }

        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {pdfDirectory}");
            return;
        }

        // Gather all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Thread‑safe collection for results
        ConcurrentBag<string> results = new ConcurrentBag<string>();

        // Limit concurrency to avoid excessive memory usage
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2) // adjust as needed
        };

        // Compare each PDF to the reference in parallel
        Parallel.ForEach(pdfFiles, options, pdfPath =>
        {
            bool isMatch = ComparePdf(referencePdfPath, pdfPath);
            string fileName = Path.GetFileName(pdfPath);
            string result = isMatch
                ? $"{fileName}: MATCH"
                : $"{fileName}: DIFFERENT";

            results.Add(result);
        });

        // Output results
        Console.WriteLine("Comparison results:");
        foreach (string line in results)
        {
            Console.WriteLine(line);
        }
    }
}