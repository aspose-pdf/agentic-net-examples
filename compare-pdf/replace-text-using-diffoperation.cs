using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "first.pdf";
        const string targetPath = "second.pdf";
        const string outputPath = "second_fixed.pdf";

        // Ensure the source PDFs exist – create simple placeholders if they are missing.
        if (!File.Exists(originalPath))
        {
            CreateSamplePdf(originalPath, "This is the original PDF. Page 1.");
        }
        if (!File.Exists(targetPath))
        {
            CreateSamplePdf(targetPath, "This is the modified PDF. Page 1 with a typo.");
        }

        // Load the PDFs.
        using (Document originalDoc = new Document(originalPath))
        using (Document targetDoc = new Document(targetPath))
        {
            int pageCount = Math.Min(originalDoc.Pages.Count, targetDoc.Pages.Count);
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Page originalPage = originalDoc.Pages[pageNumber];
                Page targetPage   = targetDoc.Pages[pageNumber];

                // Extract the full text from both pages.
                var originalAbsorber = new TextFragmentAbsorber();
                originalAbsorber.Visit(originalPage);
                var targetAbsorber   = new TextFragmentAbsorber();
                targetAbsorber.Visit(targetPage);

                string originalText = originalAbsorber.Text ?? string.Empty;
                string targetText   = targetAbsorber.Text   ?? string.Empty;

                // If the texts differ, replace the target page content with the original text.
                if (!originalText.Equals(targetText, StringComparison.Ordinal))
                {
                    // Remove existing content.
                    targetPage.Paragraphs.Clear();

                    // Create a new fragment using the original text.
                    var fragment = new TextFragment(originalText);

                    // Preserve basic style from the first fragment of the original page, if any.
                    if (originalAbsorber.TextFragments.Count > 0)
                    {
                        // TextFragment collection is 1‑based.
                        TextFragment sample = originalAbsorber.TextFragments[1];
                        fragment.Position = sample.Position;
                        fragment.TextState.Font = sample.TextState.Font;
                        fragment.TextState.FontSize = sample.TextState.FontSize;
                        fragment.TextState.ForegroundColor = sample.TextState.ForegroundColor;
                    }

                    targetPage.Paragraphs.Add(fragment);
                }
            }

            // Save the corrected PDF.
            targetDoc.Save(outputPath);
        }
    }

    // Helper method to create a very simple PDF with a single line of text.
    private static void CreateSamplePdf(string path, string text)
    {
        var doc = new Document();
        var page = doc.Pages.Add();
        var tf = new TextFragment(text)
        {
            Position = new Position(100, 700),
            TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial"), ForegroundColor = Color.Black }
        };
        page.Paragraphs.Add(tf);
        doc.Save(path);
    }
}
