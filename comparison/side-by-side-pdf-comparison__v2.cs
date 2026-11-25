using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDFs and the output comparison PDF
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "side_by_side_comparison.pdf";

        // Verify that both source files exist before proceeding
        if (!File.Exists(firstPdfPath))
        {
            Console.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents using the core Document class
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (optional)
            var options = new SideBySideComparisonOptions
            {
                // Example option – show change marks
                AdditionalChangeMarks = true
            };

            // Perform a side‑by‑side visual comparison.
            // SideBySidePdfComparer is a static class; its Compare method is static,
            // returns void and writes the result directly to the target file.
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            Console.WriteLine($"Side‑by‑side comparison saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during PDF comparison:");
            Console.WriteLine(ex.Message);
        }
    }
}
