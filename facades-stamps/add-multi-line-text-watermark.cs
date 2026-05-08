using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        // Ensure the source PDF exists; create a placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Create a multi‑line formatted text watermark.
        var ft = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",                     // first line
            System.Drawing.Color.Red,            // text color (System.Drawing)
            "Helvetica",                        // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                               // embed font flag
            48f);                                 // font size (float)

        ft.AddNewLineText("Do Not Distribute");
        ft.AddNewLineText("Company XYZ");

        // Apply the watermark to every page using PdfFileStamp.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);
            stamp.AddHeader(ft, 0); // 0 = default vertical offset
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
