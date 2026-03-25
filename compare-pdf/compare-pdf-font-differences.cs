using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Comparison options – default options already detect font changes
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) comparison
            List<DiffOperation> diffOperations = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            Console.WriteLine($"Total diff operations detected: {diffOperations.Count}");
            Console.WriteLine();

            // List each diff operation – font differences appear as separate entries
            foreach (DiffOperation diff in diffOperations)
            {
                Console.WriteLine(diff.ToString());
            }
        }
    }
}