using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a text stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Prepare formatted text for the stamp (e.g., "CONFIDENTIAL")
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                     // text
            System.Drawing.Color.Gray,          // text color (System.Drawing.Color is required)
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            36);                                // font size

        // Bind the formatted text to the stamp
        stamp.BindLogo(ft);

        // Position the stamp (example coordinates; adjust as needed)
        stamp.SetOrigin(100, 400);

        // Set opacity to 30% (0.3f) and make it a background stamp
        stamp.Opacity = 0.3f;
        stamp.IsBackground = true;

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied and saved to '{outputPath}'.");
    }
}