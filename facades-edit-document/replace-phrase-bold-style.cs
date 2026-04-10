using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcPhrase = "old phrase";
        const string destPhrase = "new phrase";

        // ------------------------------------------------------------
        // Ensure a source PDF exists. If it does not, create a minimal
        // PDF that contains the source phrase. This removes the
        // FileNotFoundException that occurs when the sample is run
        // without an external file.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document sample = new Document())
            {
                Page page = sample.Pages.Add();
                // Add a paragraph with the phrase we will replace.
                page.Paragraphs.Add(new TextFragment(srcPhrase));
                sample.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // --------------------------------------------------------
            // Determine the original font size (and font) of the first
            // occurrence of the source phrase. This information is used
            // to keep the visual appearance unchanged after replacement.
            // --------------------------------------------------------
            float originalFontSize = 12f; // fallback size
            Font? originalFont = null;    // nullable to avoid CS8600 warning

            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcPhrase);
            // Search on the first page (adjust if needed)
            doc.Pages[1].Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // TextFragments collection is 1‑based in Aspose.Pdf
                TextFragment firstFragment = absorber.TextFragments[1];
                originalFontSize = firstFragment.TextState.FontSize;
                originalFont = firstFragment.TextState.Font;
            }

            // --------------------------------------------------------
            // Prepare TextState for the replacement text: keep original
            // size, apply Bold style, and reuse the original font when
            // it is available.
            // --------------------------------------------------------
            TextState replaceState = new TextState
            {
                FontSize = originalFontSize,
                FontStyle = FontStyles.Bold
            };
            if (originalFont != null)
                replaceState.Font = originalFont;

            // --------------------------------------------------------
            // Perform the replacement on all pages (pageNumber = 0).
            // Save the document *before* disposing the editor to avoid
            // accessing a disposed Document instance.
            // --------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);
                editor.ReplaceText(srcPhrase, 0, destPhrase, replaceState);
                // Save while the editor is still alive.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Phrase replaced and saved to '{outputPath}'.");
    }
}
