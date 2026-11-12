using System;
using System.Drawing;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "wrapped_text.pdf";
        const string longText = "This is a very long paragraph that will exceed the defined width of the text box and should be wrapped by words automatically by Aspose.Pdf.";

        // Create a blank PDF with a single page.
        using (var doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(pdfPath);
        }

        // Get page count (required by PdfFileMend overloads that accept an int[] of pages).
        var info = new PdfFileInfo(pdfPath);
        int pageCount = info.NumberOfPages;
        info.Close();

        // Configure PdfFileMend for word‑wrapping.
        var mend = new PdfFileMend();
        mend.BindPdf(pdfPath);
        mend.IsWordWrap = true;                 // enable word‑wrap
        mend.WrapMode = WordWrapMode.ByWords;   // wrap by whole words

        // Rectangle where the text will be placed (lower‑left and upper‑right coordinates).
        // PdfFileMend.AddText expects **float** values, so we use float literals (or cast).
        float llx = 100f; // lower‑left X
        float lly = 500f; // lower‑left Y
        float urx = 400f; // upper‑right X (width = 300 points)
        float ury = 600f; // upper‑right Y

        // Build the formatted text that will be inserted.
        var formatted = new FormattedText(
            longText,
            System.Drawing.Color.Black,          // fully qualified to avoid ambiguity
            "Helvetica",        // font name
            EncodingType.Winansi, // encoding
            false,                // embed font?
            12);                  // font size

        // Insert the text into page 1 inside the defined rectangle.
        mend.AddText(formatted, 1, llx, lly, urx, ury);

        // Save the modified PDF (overwrites the original file).
        mend.Save(pdfPath);
        mend.Close();

        // Optional verification.
        using (var check = new Document(pdfPath))
        {
            Console.WriteLine($"Pages={check.Pages.Count}");
        }
    }
}
