using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create three FormattedText objects – each line has its own style
        // Line 1: Red, Helvetica, 36pt
        FormattedText line1 = new FormattedText(
            "Confidential",
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            36);

        // Line 2: Blue, Times New Roman, 28pt
        FormattedText line2 = new FormattedText(
            "Do Not Distribute",
            System.Drawing.Color.Blue,
            "Times New Roman",
            EncodingType.Winansi,
            false,
            28);

        // Line 3: Green, Courier New, 24pt
        FormattedText line3 = new FormattedText(
            "© 2026 Company",
            System.Drawing.Color.Green,
            "Courier New",
            EncodingType.Winansi,
            false,
            24);

        // Create a stamp for each line and position them vertically
        // All stamps are set as background with 50% opacity
        Aspose.Pdf.Facades.Stamp stamp1 = new Aspose.Pdf.Facades.Stamp();
        stamp1.BindLogo(line1);
        stamp1.IsBackground = true;
        stamp1.Opacity = 0.5f;
        stamp1.SetOrigin(100, 500); // X, Y (bottom-left origin)

        Aspose.Pdf.Facades.Stamp stamp2 = new Aspose.Pdf.Facades.Stamp();
        stamp2.BindLogo(line2);
        stamp2.IsBackground = true;
        stamp2.Opacity = 0.5f;
        stamp2.SetOrigin(100, 460); // slightly lower Y to appear below line1

        Aspose.Pdf.Facades.Stamp stamp3 = new Aspose.Pdf.Facades.Stamp();
        stamp3.BindLogo(line3);
        stamp3.IsBackground = true;
        stamp3.Opacity = 0.5f;
        stamp3.SetOrigin(100, 420); // further lower Y

        // Apply the stamps to all pages using PdfFileStamp
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);          // load source PDF
            fileStamp.AddStamp(stamp1);           // add first line
            fileStamp.AddStamp(stamp2);           // add second line
            fileStamp.AddStamp(stamp3);           // add third line
            fileStamp.Save(outputPdf);            // write result
            fileStamp.Close();
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}