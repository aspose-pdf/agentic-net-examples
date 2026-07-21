using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Initialize default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Perform a flat document comparison and save the visual diff PDF
                List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

                // Output a simple summary of the differences
                Console.WriteLine($"Comparison completed. Total differences: {diffs.Count}");
                foreach (DiffOperation diff in diffs)
                {
                    Console.WriteLine(diff);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}