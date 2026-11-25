using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string diffPath = "diff.md";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Configure comparison options – set the extraction rectangle.
            ComparisonOptions options = new ComparisonOptions
            {
                // A4 page size in points (0,0 is lower‑left corner).
                ExtractionArea = new Rectangle(0, 0, 595, 842)
            };

            // Perform a page‑by‑page text comparison.
            List<List<DiffOperation>> pageDiffs =
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate the diff in Markdown format.
            MarkdownDiffOutputGenerator generator = new MarkdownDiffOutputGenerator();
            generator.GenerateOutput(pageDiffs, diffPath);
        }

        Console.WriteLine($"Markdown diff saved to '{diffPath}'.");
    }
}
