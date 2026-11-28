using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const float bottomMargin = 15f; // points
        const float fontSize = 12f;
        const string footerText = "Page 1 of 10";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain page count and dimensions.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;
        // Assume all pages have the same width; use the first page.
        double pageWidth = doc.Pages[1].PageInfo.Width;

        // Build the array of page numbers (1‑based) to which the footer will be added.
        int[] pages = Enumerable.Range(1, pageCount).ToArray();

        // Create the formatted text. Use fully‑qualified System.Drawing.Color and the correct FontStyle enum.
        FormattedText footer = new FormattedText(
            footerText,
            System.Drawing.Color.Blue,
            Aspose.Pdf.Facades.FontStyle.Helvetica,
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            fontSize);

        // Define the rectangle where the footer will be placed.
        float lowerLeftX = 0f;
        float lowerLeftY = bottomMargin; // 15 points from the bottom edge
        float upperRightX = (float)pageWidth;
        float upperRightY = bottomMargin + fontSize + 2f; // a little extra height for the text

        // Use PdfFileMend (the class that supports AddText with FormattedText).
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPath);
            mend.AddText(footer, pages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            mend.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
