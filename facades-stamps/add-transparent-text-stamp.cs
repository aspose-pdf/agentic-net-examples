using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp
        FormattedText formatted = new FormattedText(
            stampText,
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            48);

        // Configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);
        stamp.Opacity = 0.7f; // 70% opacity
        stamp.IsBackground = true; // place behind page content
        stamp.Pages = new int[] { 1, 3 }; // selected pages (example)

        // Apply the stamp using PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Transparent text stamp applied. Saved to '{outputPath}'.");
    }
}
