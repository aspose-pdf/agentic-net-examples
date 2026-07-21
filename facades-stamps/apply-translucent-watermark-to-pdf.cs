using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize PdfFileStamp facade and specify input/output files
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.InputFile  = inputPath;
            fileStamp.OutputFile = outputPath;

            // Create a stamp with 50% opacity (translucent watermark)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp
            {
                Opacity     = 0.5f,   // 0.0 (fully transparent) to 1.0 (opaque)
                IsBackground = true   // place stamp behind page content
            };

            // Define watermark text using FormattedText (requires System.Drawing.Color)
            FormattedText watermark = new FormattedText(
                "CONFIDENTIAL",                     // text
                System.Drawing.Color.LightGray,     // text color
                "Helvetica",                        // font name
                EncodingType.Winansi,               // encoding
                false,                              // embed font
                48);                                // font size

            // Bind the text to the stamp
            stamp.BindLogo(watermark);

            // Apply the stamp to all pages
            fileStamp.AddStamp(stamp);

            // Close the facade to finalize the output file
            fileStamp.Close();
        }

        Console.WriteLine($"Translucent watermark applied. Output saved to '{outputPath}'.");
    }
}