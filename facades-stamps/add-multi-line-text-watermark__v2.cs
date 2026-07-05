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
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain the total page count.
        Document doc = new Document(inputPath);
        int[] allPages = Enumerable.Range(1, doc.Pages.Count).ToArray();

        // Use PdfFileMend (Facades) to add formatted text as a watermark.
        using (Aspose.Pdf.Facades.PdfFileMend mend = new Aspose.Pdf.Facades.PdfFileMend())
        {
            mend.BindPdf(inputPath);

            // Line 1 – largest font size
            FormattedText line1 = new FormattedText(
                "CONFIDENTIAL",
                System.Drawing.Color.Red,
                "Helvetica",
                EncodingType.Winansi,
                false,
                48f);
            // Position the first line near the top of the page.
            mend.AddText(line1, allPages, 0f, 100f, 500f, 50f);

            // Line 2 – medium font size
            FormattedText line2 = new FormattedText(
                "Do Not Distribute",
                System.Drawing.Color.Red,
                "Helvetica",
                EncodingType.Winansi,
                false,
                36f);
            mend.AddText(line2, allPages, 0f, 150f, 500f, 50f);

            // Line 3 – smaller font size
            FormattedText line3 = new FormattedText(
                "Company Name",
                System.Drawing.Color.Red,
                "Helvetica",
                EncodingType.Winansi,
                false,
                24f);
            mend.AddText(line3, allPages, 0f, 200f, 500f, 50f);

            // Persist the changes.
            mend.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
