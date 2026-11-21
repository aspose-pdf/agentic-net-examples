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

        // Verify input files exist
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

        // Load the two documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create a new PDF that will hold the comparison result
            using (Document diffDoc = new Document())
            {
                // Add a single page for the summary
                Page summaryPage = diffDoc.Pages.Add();

                // Prepare a text fragment to describe the comparison
                TextFragment tf = new TextFragment();
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.Position = new Position(50, 800); // top-left margin

                // Compare page counts
                if (doc1.Pages.Count != doc2.Pages.Count)
                {
                    tf.Text = $"Page count differs: first PDF has {doc1.Pages.Count} pages, second PDF has {doc2.Pages.Count} pages.";
                }
                else
                {
                    // Simple per‑page size comparison (width/height)
                    bool anyDifference = false;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Both PDFs contain {doc1.Pages.Count} pages. Checking page dimensions:");

                    for (int i = 1; i <= doc1.Pages.Count; i++) // 1‑based indexing
                    {
                        Page p1 = doc1.Pages[i];
                        Page p2 = doc2.Pages[i];

                        // Compare MediaBox dimensions
                        if (p1.MediaBox.Width != p2.MediaBox.Width ||
                            p1.MediaBox.Height != p2.MediaBox.Height)
                        {
                            anyDifference = true;
                            sb.AppendLine($"- Page {i}: size differs (First: {p1.MediaBox.Width}x{p1.MediaBox.Height}, Second: {p2.MediaBox.Width}x{p2.MediaBox.Height})");
                        }
                    }

                    tf.Text = anyDifference ? sb.ToString() : "All pages have identical dimensions.";
                }

                // Add the text fragment to the summary page
                summaryPage.Paragraphs.Add(tf);

                // Save the diff PDF
                diffDoc.Save(diffPdfPath);
            }
        }

        Console.WriteLine($"Comparison report saved to '{diffPdfPath}'.");
    }
}