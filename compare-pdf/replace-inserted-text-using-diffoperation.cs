using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class ReplaceChangedText
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";   // PDF with original text
        const string secondPdfPath = "second.pdf"; // PDF that may contain changed text
        const string outputPdfPath = "second_fixed.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Comparison options – default is fine for text comparison
            ComparisonOptions options = new ComparisonOptions();

            // Compare page‑by‑page (process all pages that exist in both documents)
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page page1 = doc1.Pages[pageIndex];
                Page page2 = doc2.Pages[pageIndex];

                // Get the list of differences for the current page
                List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);

                // Iterate over each diff operation
                foreach (DiffOperation diff in diffs)
                {
                    // We are interested in insertions (text that appears in the second PDF but not in the first).
                    if (diff.Operation == Operation.Insert)
                    {
                        // Text that was inserted in the second PDF
                        string insertedText = diff.Text;

                        // Corresponding original text from the first PDF (re‑assembled from the diff list)
                        string originalText = TextPdfComparer.AssemblySourcePageText(new List<DiffOperation> { diff });

                        // Find the inserted text in the second PDF and replace it with the original text
                        TextFragmentAbsorber absorber = new TextFragmentAbsorber(insertedText);
                        page2.Accept(absorber);

                        foreach (TextFragment tf in absorber.TextFragments)
                        {
                            tf.Text = originalText;
                        }
                    }
                }
            }

            // Save the modified second PDF
            doc2.Save(outputPdfPath);
        }

        Console.WriteLine($"Replaced changed text and saved to '{outputPdfPath}'.");
    }
}
