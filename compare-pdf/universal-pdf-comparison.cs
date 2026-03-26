using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPath))
            using (Document doc2 = new Document(secondPath))
            {
                ComparisonOptions options = new ComparisonOptions();
                // Customize options if needed, e.g., options.IgnoreFormatting = true;

                List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);
                Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
                Console.WriteLine($"Total differences detected: {differences.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}