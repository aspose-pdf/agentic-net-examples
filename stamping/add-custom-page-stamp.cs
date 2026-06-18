using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class AddCustomPageStamp
{
    public static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a sample PDF (input.pdf) that will receive the stamp.
        // -------------------------------------------------------------------
        using (Document inputDoc = new Document())
        {
            // First page – just regular content.
            inputDoc.Pages.Add();
            TextFragment firstPageText = new TextFragment("First page – regular content");
            inputDoc.Pages[1].Paragraphs.Add(firstPageText);

            // Second page – stamp will be placed here.
            inputDoc.Pages.Add();
            TextFragment secondPageText = new TextFragment("Second page – stamp will appear here");
            inputDoc.Pages[2].Paragraphs.Add(secondPageText);

            inputDoc.Save("input.pdf");
        }

        // -------------------------------------------------------------------
        // 2. Create a separate PDF (stamp.pdf) that will be used as the stamp source.
        // -------------------------------------------------------------------
        using (Document stampDoc = new Document())
        {
            stampDoc.Pages.Add();
            TextFragment stampPageText = new TextFragment("Stamp Content");
            stampDoc.Pages[1].Paragraphs.Add(stampPageText);
            stampDoc.Save("stamp.pdf");
        }

        // -------------------------------------------------------------------
        // 3. Load the target PDF and apply a page stamp with custom size and position.
        // -------------------------------------------------------------------
        using (Document targetDoc = new Document("input.pdf"))
        {
            using (Document sourceDoc = new Document("stamp.pdf"))
            {
                // The page that will be used as the stamp (first page of stamp.pdf).
                Page stampSourcePage = sourceDoc.Pages[1];

                // Create the PdfPageStamp from the source page.
                PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage);

                // Set custom dimensions (points).
                pageStamp.Width = 150f;
                pageStamp.Height = 100f;

                // Position the stamp 50 points from the left and 50 points from the bottom.
                pageStamp.XIndent = 50f;
                pageStamp.YIndent = 50f;

                // Apply the stamp to the second page of the target document.
                targetDoc.Pages[2].AddStamp(pageStamp);

                // Save the final document.
                targetDoc.Save("output.pdf");
            }
        }
    }
}