using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents to be compared
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Attempt to set an (optional) IgnoreCase property via reflection.
            // This property may not exist in all versions of Aspose.Pdf.
            PropertyInfo ignoreCaseProp = typeof(ComparisonOptions).GetProperty("IgnoreCase");
            if (ignoreCaseProp != null && ignoreCaseProp.CanWrite)
            {
                ignoreCaseProp.SetValue(options, true);
                Console.WriteLine("ComparisonOptions.IgnoreCase set to true.");
            }
            else
            {
                Console.WriteLine("ComparisonOptions.IgnoreCase property not found; proceeding with default case‑sensitive comparison.");
            }

            // Perform a flat document comparison and save the visual diff PDF.
            var diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);

            Console.WriteLine($"Comparison completed. Number of differences: {diffs.Count}");
        }
    }
}