using System;
using System.Drawing; // Required for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileStamp facade to add a rotated stamp
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPath);

            // Create a stamp instance (facade Aspose.Pdf.Facades.Stamp)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Define the visual content of the stamp using the correct FormattedText overload
            // Parameters: text, System.Drawing.Color, font name, encoding, embed flag, font size (float)
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                "CONFIDENTIAL",                // stamp text
                System.Drawing.Color.Red,       // text color (System.Drawing.Color)
                "Helvetica",                  // font name
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                          // embed font flag
                36f);                           // font size as float

            // Bind the formatted text to the stamp
            stamp.BindLogo(ft);

            // Apply a 45‑degree rotation for diagonal placement
            stamp.Rotation = 45f;

            // Add the configured stamp to the PDF (applies to all pages)
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Rotated stamp applied and saved to '{outputPath}'.");
    }
}
