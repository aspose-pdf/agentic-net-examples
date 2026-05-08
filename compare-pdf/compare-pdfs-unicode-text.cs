using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs that use different language encodings
        const string pdfPath1 = "doc_en.pdf";
        const string pdfPath2 = "doc_ru.pdf";

        // Path where the visual comparison result will be saved
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents (using the recommended lifecycle pattern)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create default comparison options. No special settings are required for Unicode detection;
            // Aspose.Pdf handles Unicode text automatically.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison.
            // The overload with a result path also generates a PDF that highlights the differences.
            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(
                doc1,
                doc2,
                options,
                resultPdfPath
            );

            // Report the number of detected Unicode text differences.
            Console.WriteLine($"Unicode text differences detected: {differences.Count}");

            // List each difference (operation type and affected text).
            foreach (DiffOperation diff in differences)
            {
                // DiffOperation provides the Operation type and the text that differs.
                // Page information can be obtained from diff.ToString() if needed.
                Console.WriteLine($"{diff.Operation} – \"{diff.Text}\"");
            }
        }

        // Inform the user where the visual comparison PDF was saved.
        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}
