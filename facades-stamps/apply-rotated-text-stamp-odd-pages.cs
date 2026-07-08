using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for FormattedText, EncodingType, Stamp

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine total number of pages
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array containing all odd‑numbered pages (1‑based indexing)
        int oddCount = (pageCount + 1) / 2;
        int[] oddPages = new int[oddCount];
        int idx = 0;
        for (int i = 1; i <= pageCount; i += 2)
        {
            oddPages[idx++] = i;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a formatted text object (use System.Drawing.Color and fully‑qualified FormattedText)
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "Sample Aspose.Pdf.Facades.Stamp",   // text
            System.Drawing.Color.Black,            // text color
            "Helvetica",                         // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                                 // embed font flag
            12f);                                  // font size (float)

        // Bind the formatted text to a stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);

        // Rotate the stamp by 30 degrees
        stamp.Rotation = 30f;

        // Position the stamp near the left margin (X = 10). Y = 0 places it at the bottom; adjust as needed.
        stamp.SetOrigin(10f, 0f);

        // Apply the stamp only to odd‑numbered pages
        stamp.Pages = oddPages;

        // Add the stamp to the document and save the result
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Rotated text stamp applied to odd pages. Output saved to '{outputPath}'.");
    }
}
