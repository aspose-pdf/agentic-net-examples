using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the PDF document – PdfFileStamp works with a Document instance.
        Document doc = new Document(inputPath);

        // PdfFileStamp implements IDisposable – wrap it in a using block.
        using (PdfFileStamp stamp = new PdfFileStamp(doc))
        {
            // Use System.Drawing.Color (fully qualified) to avoid ambiguity with Aspose.Pdf.Color.
            // The FormattedText constructor that matches the provided arguments expects a FontStyle enum.
            // Tahoma is not a built‑in FontStyle; we fall back to a standard style (e.g., Helvetica).
            FormattedText footer = new FormattedText(
                "Confidential Footer",
                System.Drawing.Color.DarkRed,   // text color
                FontStyle.Helvetica,            // font style (fallback for Tahoma)
                EncodingType.Winansi,
                false,                           // embed the font?
                12);                             // font size

            // Add the footer to every page with a bottom margin of 14 points.
            stamp.AddFooter(footer, 14f);

            // Save the stamped PDF.
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
