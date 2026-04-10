using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoMessage = "Special Offer: 20% OFF!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade for adding text
        Aspose.Pdf.Facades.PdfFileMend mend = new Aspose.Pdf.Facades.PdfFileMend();

        // Load the source PDF
        mend.BindPdf(inputPath);

        // Prepare formatted text (red Helvetica, size 24)
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            promoMessage,
            System.Drawing.Color.Red,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            24);

        // Pages on which the text will be placed (3, 5, 7)
        int[] targetPages = new int[] { 3, 5, 7 };

        // Add the text to the specified pages within the rectangle (100,500)-(300,550)
        mend.AddText(formattedText, targetPages, 100f, 500f, 300f, 550f);

        // Save the modified PDF
        mend.Save(outputPath);

        // Release resources
        mend.Close();

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Saved as '{outputPath}'.");
    }
}