using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string leftPdf = "left.pdf";
        const string rightPdf = "right.pdf";
        const string visualResult = "comparison_result.pdf";
        const string jsonReport = "diff_report.json";

        if (!File.Exists(leftPdf) || !File.Exists(rightPdf))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the PDF documents
        using (Document leftDoc = new Document(leftPdf))
        using (Document rightDoc = new Document(rightPdf))
        {
            // Optional visual side‑by‑side comparison (static method, no instance needed)
            SideBySidePdfComparer.Compare(leftDoc, rightDoc, visualResult, new SideBySideComparisonOptions());

            // Perform text‑based comparison that returns a list of diff lists (one per page)
            var compareOptions = new ComparisonOptions(); // default options are sufficient

            // The API returns List<List<DiffOperation>> which cannot be assigned directly to
            // IList<IList<DiffOperation>> because generic variance is not supported.
            // Capture the result with var (or List<List<DiffOperation>>) and then work with it.
            var pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(leftDoc, rightDoc, compareOptions);

            // Flatten the per‑page diff collections into a single list
            List<DiffOperation> allDiffs = pageDiffs.SelectMany(p => p).ToList();

            // Serialize the diff information to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(allDiffs, jsonOptions);
            File.WriteAllText(jsonReport, json);
        }

        Console.WriteLine($"Comparison completed. JSON diff report saved to '{jsonReport}'.");
    }
}
