using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "doc1.pdf";
        const string file2 = "doc2.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Define rectangular regions to exclude from each document
        Aspose.Pdf.Rectangle[] excludedFirst = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(100, 500, 300, 600) // left, bottom, right, top
        };

        Aspose.Pdf.Rectangle[] excludedSecond = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(200, 400, 350, 550)
        };

        // Configure comparison options with the excluded areas
        ComparisonOptions options = new ComparisonOptions
        {
            ExcludeAreas1 = excludedFirst,
            ExcludeAreas2 = excludedSecond,
            ExcludeTables = false
        };

        try
        {
            using (Document doc1 = new Document(file1))
            using (Document doc2 = new Document(file2))
            {
                // Perform page‑by‑page comparison respecting the excluded rectangles
                List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);
                Console.WriteLine($"Comparison completed. Pages with differences: {diffs.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}