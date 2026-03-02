using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the input PDF files and the output comparison PDF.
        const string firstPdfPath  = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";
        const string resultPdfPath = "ComparisonResult.pdf";

        // Verify that both source files exist before proceeding.
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

        // Choose the comparison mode:
        //   true  – compare the entire documents.
        //   false – compare specific pages (example: first page of each document).
        bool compareWholeDocument = true; // change as needed

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                var comparer = new GraphicalPdfComparer();

                if (compareWholeDocument)
                {
                    // Compare the whole documents and write the visual diff to a PDF file.
                    comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
                    Console.WriteLine($"Document comparison saved to '{resultPdfPath}'.");
                }
                else
                {
                    // Example: compare the first page of each document.
                    // Aspose.Pdf uses 1‑based page indexing.
                    if (doc1.Pages.Count < 1 || doc2.Pages.Count < 1)
                    {
                        Console.Error.WriteLine("One of the documents does not contain any pages.");
                        return;
                    }

                    Page page1 = doc1.Pages[1];
                    Page page2 = doc2.Pages[1];

                    // Compare the selected pages and write the visual diff to a PDF file.
                    comparer.ComparePagesToPdf(page1, page2, resultPdfPath);
                    Console.WriteLine($"Page comparison saved to '{resultPdfPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}