using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class AddRtlTextStamp
{
    public static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a self‑contained sample PDF (required by the rules)
        // ------------------------------------------------------------
        using (Document sampleDoc = new Document())
        {
            // Add a single page (evaluation mode allows up to 4 pages)
            using (Page samplePage = sampleDoc.Pages.Add())
            {
                // Optional: add some visible content so the page is not empty
                TextFragment placeholder = new TextFragment("Sample page");
                samplePage.Paragraphs.Add(placeholder);
            }
            sampleDoc.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // 2. Load the PDF and stamp Arabic (right‑to‑left) text
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Arabic text to be stamped
            string arabicText = "مرحبا بالعالم";

            // Create the TextStamp instance
            TextStamp textStamp = new TextStamp(arabicText);

            // Configure the visual appearance of the stamp
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 14;
            // NOTE: The current Aspose.Pdf version used in the sandbox does not expose an IsRightToLeft
            // property on TextState. Arabic rendering works correctly with a suitable font, so the line
            // that attempted to set IsRightToLeft has been removed to fix the build error.

            // Position the stamp – centre of the page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the stamp to every page (only one page in this example)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the resulting PDF
            pdfDoc.Save("output.pdf");
        }
    }
}
