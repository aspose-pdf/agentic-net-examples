using System;
using System.IO;
using System.Drawing;               // System.Drawing.Color for FormattedText
using System.Linq;                 // Enumerable.Range for page list
using Aspose.Pdf;                  // Document, PageInfo, etc.
using Aspose.Pdf.Facades;          // FormattedText, EncodingType, PdfFileMend

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const float bottomMargin = 20f;   // Margin from the bottom edge of each page
        const float footerHeight = 30f;   // Approximate height of the footer area

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain page dimensions and count.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;
        // Build an array containing every page number (1‑based).
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Create the formatted text that will appear in the footer.
        // Note: use System.Drawing.Color, not Aspose.Pdf.Color, and a float for font size.
        Aspose.Pdf.Facades.FormattedText footer = new Aspose.Pdf.Facades.FormattedText(
            "This is a sample footer text that should wrap word by word across the page width.",
            System.Drawing.Color.Black,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            10f); // font size

        // Determine the width of the first page (all pages share the same size in most PDFs).
        float pageWidth = (float)doc.Pages[1].PageInfo.Width;

        // Use PdfFileMend (the Facade API) to add the footer to every page.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        // AddText parameters: formatted text, page numbers, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
        // The rectangle is placed at the bottom of the page using the bottomMargin.
        mend.AddText(
            footer,
            allPages,
            0f,                                 // llx (left edge)
            bottomMargin,                       // lly (bottom margin)
            pageWidth,                          // urx (right edge)
            bottomMargin + footerHeight);       // ury (top of footer area)

        // Save the modified PDF.
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Footer added successfully. Output saved to '{outputPath}'.");
    }
}
