using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoText = "Buy now and get 20% off!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Prepare formatted text (font, size, colour)
            FormattedText formatted = new FormattedText(
                promoText,
                System.Drawing.Color.Blue, // fully qualified to avoid ambiguity
                "Helvetica",            // font name (must be available)
                EncodingType.Winansi,
                false,                    // embed font?
                12f);                     // font size

            // Pages on which the text will be added (1‑based indexing)
            int[] pages = { 3, 5, 7 };

            // Define the rectangle where the text will be placed.
            // Lower‑left (llx, lly) and upper‑right (urx, ury) coordinates.
            float llx = 50f;   // 50 points from the left edge
            float lly = 50f;   // 50 points from the bottom edge
            float urx = 250f;  // width of 200 points
            float ury = 100f;  // height of 50 points

            // Use PdfFileMend which supports AddText for multiple pages.
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(inputPath);
            mend.AddText(formatted, pages, llx, lly, urx, ury);
            mend.Save(outputPath);
            mend.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
