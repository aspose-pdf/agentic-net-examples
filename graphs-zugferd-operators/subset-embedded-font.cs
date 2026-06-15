using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: create a sample PDF that uses a custom font and embed it
        using (Document document = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = document.Pages.Add();

            // Locate the custom font (e.g., Arial) and mark it for embedding
            Font customFont = FontRepository.FindFont("Arial");
            customFont.IsEmbedded = true;

            // Create a text fragment that uses the custom font
            TextFragment fragment = new TextFragment("Hello, subsetted font!");
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 24;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document as the source file
            document.Save("input.pdf");
        }

        // Step 2: reopen the PDF and subset the embedded font to keep only used glyphs
        using (Document document = new Document("input.pdf"))
        {
            // Subset all fonts (including the already embedded custom font)
            document.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // Save the optimized PDF
            document.Save("output.pdf");
        }
    }
}