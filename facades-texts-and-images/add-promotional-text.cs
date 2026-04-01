using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoMessage = "Special Offer!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text – color must be System.Drawing.Color, not Aspose.Pdf.Color
        FormattedText formatted = new FormattedText(
            promoMessage,
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24f);

        int[] targetPages = new int[] { 3, 5, 7 };
        float lowerLeftX = 100f;
        float lowerLeftY = 500f;
        float upperRightX = 300f;
        float upperRightY = 550f;

        // Use PdfFileMend (not PdfFileStamp) for AddText operation
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        mend.AddText(formatted, targetPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Promotional text added to pages 3, 5, 7 and saved as '{outputPath}'.");
    }
}
