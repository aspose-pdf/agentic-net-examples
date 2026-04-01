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
        const string promoMessage = "Special Offer: 20% OFF!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the promotional message
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            promoMessage,
            System.Drawing.Color.Red,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            24);

        // Pages on which the text will be added
        int[] targetPages = new int[] { 3, 5, 7 };

        // Define the rectangle area (lower‑left and upper‑right coordinates) in points
        float lowerLeftX = 100f;
        float lowerLeftY = 500f;
        float upperRightX = 300f;
        float upperRightY = 550f;

        // Use PdfFileMend to add the text to the specified pages
        Aspose.Pdf.Facades.PdfFileMend mender = new Aspose.Pdf.Facades.PdfFileMend();
        mender.BindPdf(inputPath);
        bool success = mender.AddText(formattedText, targetPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add text to the PDF.");
        }
        mender.Save(outputPath);
        mender.Close();

        Console.WriteLine($"Promotional message added to pages 3, 5, 7. Saved as '{outputPath}'.");
    }
}
