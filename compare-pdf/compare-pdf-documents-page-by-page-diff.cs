using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string diffPdfPath   = "diff.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load the two source documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Determine the larger page count to cover all pages.
            int maxPages = Math.Max(doc1.Pages.Count, doc2.Pages.Count);

            // Create a new document that will hold the differences.
            using (Document diffDoc = new Document())
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
                for (int pageIndex = 1; pageIndex <= maxPages; pageIndex++)
                {
                    string text1 = ExtractPageText(doc1, pageIndex);
                    string text2 = ExtractPageText(doc2, pageIndex);

                    // If the texts are different (or one page is missing), record the diff.
                    if (!string.Equals(text1, text2, StringComparison.Ordinal))
                    {
                        // Add a new blank page to the diff document.
                        Page diffPage = diffDoc.Pages.Add();

                        // Prepare a description of the difference.
                        string diffInfo = $"Page {pageIndex} differs.\n\n" +
                                          $"--- {Path.GetFileName(firstPdfPath)} ---\n{text1}\n\n" +
                                          $"--- {Path.GetFileName(secondPdfPath)} ---\n{text2}";

                        // Add the description as a text fragment.
                        TextFragment tf = new TextFragment(diffInfo)
                        {
                            // Position the text near the top‑left corner.
                            Position = new Position(50, diffPage.PageInfo.Height - 50),
                            // Use a readable font size.
                            TextState = { FontSize = 12 }
                        };
                        diffPage.Paragraphs.Add(tf);
                    }
                }

                // Save the diff document. No SaveOptions are needed because the output is PDF.
                diffDoc.Save(diffPdfPath);
                Console.WriteLine($"Diff PDF saved to '{diffPdfPath}'.");
            }
        }
    }

    // Helper method to extract text from a specific page.
    // Returns an empty string if the page does not exist.
    private static string ExtractPageText(Document doc, int pageNumber)
    {
        if (pageNumber > doc.Pages.Count)
            return string.Empty;

        TextAbsorber absorber = new TextAbsorber();
        // Extract text from the specified page only.
        doc.Pages[pageNumber].Accept(absorber);
        return absorber.Text ?? string.Empty;
    }
}