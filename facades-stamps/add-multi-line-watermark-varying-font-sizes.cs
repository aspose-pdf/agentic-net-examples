using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        // ------------------------------------------------------------
        // Create a minimal source PDF so the example can run in a sandbox
        // ------------------------------------------------------------
        using (var seed = new Document())
        {
            // Add a single blank page (you could add more content if needed)
            seed.Pages.Add();
            // Save it to the expected input path
            seed.Save(inputPdf);
        }

        // Load the source document to obtain page dimensions and count
        Document doc = new Document(inputPdf);
        int pageCount = doc.Pages.Count;
        int[] allPages = new int[pageCount];
        for (int i = 0; i < pageCount; i++)
            allPages[i] = i + 1; // pages are 1‑based

        // Bind the PdfFileMend facade to the source PDF
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Layout parameters (points)
        float topMargin = 100f;               // distance from the top edge of the page
        float lineSpacing = 30f;               // space between successive lines
        float leftMargin = 0f;                 // left edge (adjust as needed)
        float rightMargin = (float)doc.Pages[1].PageInfo.Width; // page width as float

        // Helper that adds a single line of watermark text
        void AddWatermarkLine(string text, System.Drawing.Color color, float fontSize, float yOffsetFromTop)
        {
            // Convert page height (double) to float for calculations
            float pageHeight = (float)doc.Pages[1].PageInfo.Height;

            // Upper‑right Y coordinate (measured from the top of the page)
            float ury = pageHeight - yOffsetFromTop;
            // Lower‑left Y coordinate – give the rectangle a height roughly equal to the font size
            float lly = ury - fontSize;
            float llx = leftMargin;
            float urx = rightMargin;

            var formattedText = new FormattedText(
                text,
                color,                     // System.Drawing.Color
                "Helvetica",
                EncodingType.Winansi,
                false,
                fontSize);

            // Add the text to all pages
            mend.AddText(formattedText, allPages, llx, lly, urx, ury);
        }

        // ----- First line (largest font) -----
        AddWatermarkLine("CONFIDENTIAL", System.Drawing.Color.Red, 48f, topMargin);

        // ----- Second line (medium font) -----
        AddWatermarkLine("Do Not Distribute", System.Drawing.Color.Blue, 36f, topMargin + lineSpacing);

        // ----- Third line (smallest font) -----
        AddWatermarkLine("Company XYZ", System.Drawing.Color.Green, 24f, topMargin + 2 * lineSpacing);

        // Save the modified PDF
        mend.Save(outputPdf);
        mend.Close();
    }
}
