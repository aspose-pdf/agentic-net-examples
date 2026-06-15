using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc_en.pdf";
        const string pdfPath2 = "doc_ru.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Set up comparison options (default values are sufficient for Unicode text)
                ComparisonOptions options = new ComparisonOptions();

                // Perform a flat (whole‑document) text comparison
                List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

                Console.WriteLine($"Total Unicode text differences detected: {differences.Count}");
                foreach (DiffOperation diff in differences)
                {
                    // DiffOperation implements a useful ToString; otherwise you can inspect its properties
                    Console.WriteLine(diff);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Comparison failed: {ex.Message}");
        }
    }
}