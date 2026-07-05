using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class ReplaceTextOnPageTwo
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string srcString   = "OldText";   // text to replace
        const string destString  = "NewText";   // replacement text

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF in memory with two pages.
        //    Page 2 contains the text we want to replace.
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank first page.
            doc.Pages.Add();

            // Add the second page and place the source text on it.
            Page page2 = doc.Pages.Add();
            TextFragment tf = new TextFragment(srcString);
            // Define an explicit style so we can later verify that it is preserved.
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.FontSize = 14;
            tf.TextState.FontStyle = FontStyles.Bold;
            tf.TextState.ForegroundColor = Color.Black;
            page2.Paragraphs.Add(tf);

            // -----------------------------------------------------------------
            // 2. Determine the original text style on page 2.
            // -----------------------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcString);
            // Accept only on page 2 (1‑based index).
            doc.Pages[2].Accept(absorber);

            TextState originalState = new TextState();
            if (absorber.TextFragments.Count > 0)
            {
                // TextFragment collection is 1‑based.
                TextFragment firstFragment = absorber.TextFragments[1];
                originalState.Font            = firstFragment.TextState.Font;
                originalState.FontSize        = firstFragment.TextState.FontSize;
                originalState.FontStyle       = firstFragment.TextState.FontStyle;
                originalState.ForegroundColor = firstFragment.TextState.ForegroundColor;
            }

            // -----------------------------------------------------------------
            // 3. Replace the text on page 2 while preserving the captured style.
            // -----------------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);
                // ReplaceText(source, pageNumber, destination, TextState)
                editor.ReplaceText(srcString, 2, destString, originalState);
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
