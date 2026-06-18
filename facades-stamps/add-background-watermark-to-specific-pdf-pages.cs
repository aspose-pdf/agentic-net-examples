using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color in FormattedText
using Aspose.Pdf.Facades; // Facade API for stamping

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the source PDF to the PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a text stamp (watermark) using FormattedText
            // Parameters: text, text color, font name, encoding, isBold, font size
            FormattedText ft = new FormattedText(
                "CONFIDENTIAL",
                Color.Gray,               // System.Drawing.Color
                "Helvetica",
                EncodingType.Winansi,
                false,
                48);

            // Configure the stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);                 // bind the formatted text as the stamp content
            stamp.IsBackground = true;          // render behind existing page content
            stamp.Pages = new int[] { 2, 3, 4, 5 }; // apply only to pages 2‑5
            stamp.SetOrigin(100, 400);          // position of the stamp (lower‑left corner)

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
            fileStamp.Close(); // optional, Close() is called by Dispose()
        }

        Console.WriteLine($"Background watermark applied and saved to '{outputPdf}'.");
    }
}