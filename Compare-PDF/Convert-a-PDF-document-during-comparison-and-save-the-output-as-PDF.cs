using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files – replace with your actual file paths or pass them as arguments
        string firstPdfPath = args.Length > 0 ? args[0] : "first.pdf";
        string secondPdfPath = args.Length > 1 ? args[1] : "second.pdf";

        // Output PDF that will contain the comparison result
        string outputPdfPath = "ComparisonResult.pdf";

        // Verify that the input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two documents to be compared
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Set up comparison options (customize as needed)
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: exclude tables from comparison
                ExcludeTables = false
                // Additional options can be set here
            };

            // Perform page‑by‑page text comparison and save the result as a PDF
            // This overload handles the saving internally, complying with the lifecycle rule.
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options, outputPdfPath);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}