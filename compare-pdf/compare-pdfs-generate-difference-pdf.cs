using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files (replace with actual paths)
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        // Path where the comparison result PDF will be saved
        const string resultPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the first PDF document
            using (Document doc1 = new Document(pdfPath1))
            // Load the second PDF document
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure comparison options (default settings are sufficient for most scenarios)
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: ignore tables during comparison (set to true if needed)
                    ExcludeTables = false,
                    // Example: set extraction area to null (compare whole pages)
                    ExtractionArea = null
                };

                // Perform a flat (whole‑document) text comparison.
                // The overload with a result path also creates a PDF showing the differences.
                var diffOperations = TextPdfComparer.CompareFlatDocuments(
                    doc1,
                    doc2,
                    options,
                    resultPath);

                // diffOperations contains a list of detected changes.
                Console.WriteLine($"Comparison completed. {diffOperations.Count} change(s) detected.");
                Console.WriteLine($"Result PDF saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}