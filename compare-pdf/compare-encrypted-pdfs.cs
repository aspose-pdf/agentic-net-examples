using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "encrypted1.pdf";
        const string secondPath = "encrypted2.pdf";
        const string userPassword = "user123"; // password for both PDFs (adjust as needed)
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input files were not found.");
            return;
        }

        // Load the encrypted PDFs by providing the password to the Document constructor
        using (Document doc1 = new Document(firstPath, userPassword))
        using (Document doc2 = new Document(secondPath, userPassword))
        {
            // Comparison options – default settings are sufficient for a basic diff
            ComparisonOptions options = new ComparisonOptions();

            // Perform page‑by‑page comparison and save the visual diff to a PDF file
            var changes = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            Console.WriteLine($"Pages with differences: {changes.Count}");
        }
    }
}