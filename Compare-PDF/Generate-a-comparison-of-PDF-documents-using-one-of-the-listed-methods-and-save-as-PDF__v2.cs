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
        const string resultPath = "comparison.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPath))
            using (Document doc2 = new Document(secondPath))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Show change marks that appear on other pages
                    AdditionalChangeMarks = true,
                    // Ignore spaces to focus on textual changes
                    ComparisonMode = ComparisonMode.IgnoreSpaces
                };

                // Perform the comparison and save the resulting PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}