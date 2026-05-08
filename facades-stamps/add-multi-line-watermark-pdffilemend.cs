using System;
using System.IO;
using System.Drawing; // System.Drawing.Color for Facades.FormattedText
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for EncodingType, FormattedText, PdfFileMend

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Prepare an array with all page numbers (1‑based)
            int pageCount = doc.Pages.Count;
            int[] allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                allPages[i] = i + 1;

            // Use PdfFileMend (Facades) – it works with Facades.FormattedText
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);

            // -----------------------------------------------------------------
            // First line – bold red Helvetica, size 36
            // -----------------------------------------------------------------
            FormattedText line1 = new FormattedText(
                "Confidential",
                System.Drawing.Color.Red,          // System.Drawing.Color required by Facades.FormattedText
                "Helvetica",
                EncodingType.Winansi,
                false,
                36f);                               // font size as float
            // Add the text to every page. Rectangle coordinates are (llx, lly, urx, ury).
            mend.AddText(line1, allPages, 100f, 700f, 500f, 720f);

            // -----------------------------------------------------------------
            // Second line – green Times New Roman, size 28 (with an extra blank line)
            // -----------------------------------------------------------------
            FormattedText line2 = new FormattedText(
                "Do Not Distribute",
                System.Drawing.Color.Green,
                "TimesNewRoman",
                EncodingType.Winansi,
                false,
                28f);
            // Add a blank line with extra spacing using AddNewLineText
            line2.AddNewLineText(string.Empty, 10f);
            mend.AddText(line2, allPages, 100f, 660f, 500f, 680f);

            // -----------------------------------------------------------------
            // Third line – blue Courier, size 24 with a subtitle underneath
            // -----------------------------------------------------------------
            FormattedText line3 = new FormattedText(
                "Internal Use Only",
                System.Drawing.Color.Blue,
                "Courier",
                EncodingType.Winansi,
                false,
                24f);
            // Add a second line (subtitle) inside the same FormattedText instance
            line3.AddNewLineText("Version 1.0", 5f);
            mend.AddText(line3, allPages, 100f, 620f, 500f, 640f);

            // Save the watermarked PDF
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
