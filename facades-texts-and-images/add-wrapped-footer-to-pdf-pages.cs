using System;
using System.IO;
using System.Drawing;               // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;               // needed for FormattedText and EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "footer_added.pdf";
        const string footerTemplate = "Confidential – Company XYZ – Page {page} of {pages}";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get the number of pages using PdfFileInfo (light‑weight, no full Document load)
        var fileInfo = new PdfFileInfo(inputPath);
        int pageCount = fileInfo.NumberOfPages;
        fileInfo.Close();

        // ------------------------------------------------------------
        // Use PdfFileMend for word‑by‑word wrapping. PdfContentEditor
        // does not expose a TextWrapping property.
        // ------------------------------------------------------------
        var mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        mend.IsWordWrap = true;                     // enable wrapping
        mend.WrapMode   = WordWrapMode.ByWords;     // wrap by words

        // Footer rectangle (coordinates are in points; origin is lower‑left)
        // Use float literals because PdfFileMend.AddText expects float values.
        const float llx = 0f;      // lower‑left X
        const float lly = 0f;      // lower‑left Y (bottom margin)
        const float urx = 595f;    // upper‑right X (page width)
        const float ury = 30f;     // upper‑right Y (footer height)

        // Loop through each page and add the formatted footer text.
        for (int page = 1; page <= pageCount; page++)
        {
            string pageFooter = footerTemplate
                .Replace("{page}", page.ToString())
                .Replace("{pages}", pageCount.ToString());

            // FormattedText expects System.Drawing.Color, font name, encoding, etc.
            var formattedFooter = new FormattedText(
                pageFooter,
                System.Drawing.Color.DarkGray,   // text colour
                "Arial",                         // font name
                EncodingType.Winansi,
                false,                            // isBold
                10);                              // font size

            // Add the text to the current page within the defined rectangle.
            mend.AddText(formattedFooter, page, llx, lly, urx, ury);
        }

        // Save the modified PDF.
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Footer added to all pages. Output saved to '{outputPath}'.");
    }
}
