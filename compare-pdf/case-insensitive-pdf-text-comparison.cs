using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load both documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Normalize text to lower case in both documents.
            // This effectively makes the comparison case‑insensitive.
            NormalizeDocumentTextToLowerCase(doc1);
            NormalizeDocumentTextToLowerCase(doc2);

            // Set up comparison options (no special options needed for case‑insensitivity
            // because we already normalized the text).
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) comparison.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);

            // Output a simple summary.
            Console.WriteLine($"Comparison completed. Differences found: {diffs.Count}");
            Console.WriteLine($"Result PDF saved to: {resultPdfPath}");
        }
    }

    // Helper method: converts all text fragments in a document to lower case.
    private static void NormalizeDocumentTextToLowerCase(Document doc)
    {
        // Create an absorber that extracts all text fragments.
        TextFragmentAbsorber absorber = new TextFragmentAbsorber();

        // Apply the absorber to the whole document.
        doc.Pages.Accept(absorber);

        // Iterate over each fragment and replace its text with a lower‑case version.
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            if (!string.IsNullOrEmpty(fragment.Text))
            {
                fragment.Text = fragment.Text.ToLowerInvariant();
            }
        }
    }
}