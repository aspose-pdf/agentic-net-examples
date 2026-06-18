using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class ReplaceChangedText
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";   // PDF with original text
        const string secondPdfPath = "second.pdf";  // PDF that contains changes
        const string outputPdfPath = "second_fixed.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Ensure both documents have the same number of pages for page‑by‑page comparison
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            for (int i = 1; i <= pageCount; i++)
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                // Compare the two pages and obtain the list of differences
                ComparisonOptions options = new ComparisonOptions();
                var diffs = TextPdfComparer.ComparePages(page1, page2, options);

                // Reconstruct the original (source) text from the list of differences
                string originalText = TextPdfComparer.AssemblySourcePageText(diffs);

                // Replace the entire content of the second page with the original text
                page2.Paragraphs.Clear();
                page2.Paragraphs.Add(new TextFragment(originalText));
            }

            // Save the modified second document
            doc2.Save(outputPdfPath);
        }

        Console.WriteLine($"Changed text replaced. Output saved to '{outputPdfPath}'.");
    }
}