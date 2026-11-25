using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPath))
        using (Document doc2 = new Document(secondPath))
        {
            // Set comparison options: show change marks and ignore spaces
            var options = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = true,
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // SideBySidePdfComparer is a static class – call its static Compare method.
            // The overload requires the target PDF path (or a Stream) and the options.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
    }
}
