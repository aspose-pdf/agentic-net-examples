using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoText  = "Special Offer: Get 20% off on all items!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages where the promotional message will be placed (1‑based indexing)
        int[] targetPages = new int[] { 3, 5, 7 };

        // Define the rectangle where the text will appear (lower‑left and upper‑right coordinates)
        float lowerLeftX  = 100f; // distance from left edge
        float lowerLeftY  = 700f; // distance from bottom edge
        float upperRightX = 500f; // width of the text box
        float upperRightY = 750f; // height of the text box

        // Create formatted text. Note: FormattedText uses System.Drawing.Color for the color parameter.
        FormattedText ft = new FormattedText(
            promoText,
            System.Drawing.Color.Black,   // text color
            "Helvetica",                  // font name
            EncodingType.Winansi,         // encoding
            false,                        // embed font?
            12);                          // font size

        // Use PdfFileMend to add the text to the specified pages.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF.
            mend.BindPdf(inputPath);

            // Add the promotional text to the selected pages.
            // The method signature:
            // AddText(FormattedText text, int[] pageNums,
            //         float lowerLeftX, float lowerLeftY,
            //         float upperRightX, float upperRightY)
            mend.AddText(ft, targetPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified document.
            mend.Save(outputPath);
        }

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Output saved to '{outputPath}'.");
    }
}