using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string markdownPath  = "comparison.md";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure comparison to prioritize delete operations and use flat‑text mode
            ComparisonOptions compOptions = new ComparisonOptions
            {
                EditOperationsOrder = EditOperationsOrder.DeleteFirst
                // No ComparisonType property – the flat‑text comparer is selected by the API we call
            };

            // Perform a flat‑text comparison – returns a list of diff operations per page
            List<List<DiffOperation>> pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, compOptions);

            // Generate Markdown output from the diff operations
            MarkdownDiffOutputGenerator mdGenerator = new MarkdownDiffOutputGenerator();
            mdGenerator.GenerateOutput(pageDiffs, markdownPath);
        }

        Console.WriteLine($"Comparison saved to '{markdownPath}'.");
    }
}
