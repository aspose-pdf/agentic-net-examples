using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string promotionalMessage = "Buy our product now!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create formatted text (color is System.Drawing.Color)
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            promotionalMessage,
            System.Drawing.Color.Red,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            24);

        // Pages on which the text will be placed (1‑based indexing)
        int[] targetPages = new int[] { 3, 5, 7 };

        // Position where the text will appear (lower‑left corner of the rectangle)
        float lowerLeftX = 100.0f;
        float lowerLeftY = 100.0f;
        // Define an upper‑right corner to give the text a bounding rectangle
        float upperRightX = lowerLeftX + 200.0f; // width of 200 points
        float upperRightY = lowerLeftY + 50.0f;  // height of 50 points

        // Use PdfFileMend to add the same text to multiple pages in one call
        Aspose.Pdf.Facades.PdfFileMend mend = new Aspose.Pdf.Facades.PdfFileMend();
        mend.BindPdf(inputPath);
        mend.AddText(formatted, targetPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        mend.Save(outputPath);

        Console.WriteLine($"Promotional text added to pages 3, 5, and 7. Saved as '{outputPath}'.");
    }
}
