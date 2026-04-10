using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF files
        const string pdfPath1 = "encrypted1.pdf";
        const string pdfPath2 = "encrypted2.pdf";

        // Password that unlocks both documents (adjust as needed)
        const string password = "user123";

        // Verify that the input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Open the encrypted PDFs by providing the password to the Document constructor
        using (Document doc1 = new Document(pdfPath1, password))
        using (Document doc2 = new Document(pdfPath2, password))
        {
            // Create default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);
            Console.WriteLine($"Flat comparison detected {diffs.Count} differences.");

            // Generate a visual side‑by‑side PDF comparison result
            const string resultPath = "comparison_result.pdf";
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, sideOptions);
            Console.WriteLine($"Side‑by‑side comparison saved to '{resultPath}'.");
        }
    }
}