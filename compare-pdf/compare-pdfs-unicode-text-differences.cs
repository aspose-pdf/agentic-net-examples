using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc_en.pdf";
        const string pdfPath2 = "doc_ru.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options (default settings are sufficient for Unicode detection)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat text comparison and save a visual diff PDF
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);

            // Output the detected differences
            Console.WriteLine($"Detected {diffs.Count} text differences:");
            foreach (DiffOperation diff in diffs)
            {
                Console.WriteLine(diff);
            }
        }

        Console.WriteLine($"Comparison result saved to '{resultPdfPath}'.");
    }
}