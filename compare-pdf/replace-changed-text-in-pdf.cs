using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class ReplaceChangedText
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";   // PDF with original text
        const string secondPdfPath = "second.pdf";  // PDF that may contain changes
        const string outputPdfPath = "replaced.pdf"; // Result PDF

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Prepare comparison options (default works for text comparison)
            ComparisonOptions options = new ComparisonOptions();

            // Create a new document that will contain the restored text
            using (Document resultDoc = new Document())
            {
                // Iterate over pages (1‑based indexing)
                int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page1 = doc1.Pages[i];
                    Page page2 = doc2.Pages[i];

                    // Compare the two pages and obtain the list of differences
                    List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);

                    // Restore the original (source) text from the list of differences
                    string originalText = TextPdfComparer.AssemblySourcePageText(diffs);

                    // If no differences were found, fall back to the whole page text
                    if (string.IsNullOrEmpty(originalText))
                    {
                        // Extract full text from the original page as a safety net
                        TextAbsorber absorber = new TextAbsorber();
                        page1.Accept(absorber);
                        originalText = absorber.Text;
                    }

                    // Add a new page to the result document
                    Page newPage = resultDoc.Pages.Add();

                    // Add the restored text as a TextFragment
                    TextFragment tf = new TextFragment(originalText);
                    tf.Position = new Position(50, 750); // place near top‑left
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    newPage.Paragraphs.Add(tf);
                }

                // Save the resulting PDF (lifecycle rule)
                resultDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Replaced PDF saved to '{outputPdfPath}'.");
    }
}