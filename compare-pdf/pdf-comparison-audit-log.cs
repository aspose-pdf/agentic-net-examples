using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonAudit
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string logFilePath   = "comparison_audit.txt";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Set up default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page text comparison
                // Returns a list where each element corresponds to a page
                List<List<DiffOperation>> diffsByPage =
                    TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

                // Write audit information to a text file
                using (StreamWriter writer = new StreamWriter(logFilePath, false))
                {
                    for (int i = 0; i < diffsByPage.Count; i++)
                    {
                        int pageNumber = i + 1; // Pages are 1‑based
                        foreach (DiffOperation diff in diffsByPage[i])
                        {
                            // DiffOperation exposes the type of change via the Operation property
                            writer.WriteLine($"Page {pageNumber}: {diff.Operation}");
                        }
                    }
                }

                Console.WriteLine($"Comparison audit saved to '{logFilePath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}