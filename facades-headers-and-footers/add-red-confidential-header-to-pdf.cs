using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        try
        {
            // Load the document to obtain page dimensions (needed for top‑margin placement).
            Document doc = new Document(inputPath);
            // Assume all pages have the same height – use the first page as reference.
            float pageHeight = (float)doc.Pages[1].PageInfo.Height;

            // Create formatted text: red "Confidential", Helvetica, size 16.
            var formatted = new FormattedText(
                "Confidential",
                System.Drawing.Color.Red,
                Aspose.Pdf.Facades.FontStyle.Helvetica,
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                16f);

            // Create a Facades stamp and bind the formatted text.
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formatted);

            // Position the stamp at the top of each page with a 5‑point top margin.
            // SetOrigin uses the lower‑left corner, so Y = pageHeight - topMargin.
            stamp.SetOrigin(0f, pageHeight - 5f);

            // Apply the stamp to the whole document.
            var pdfStamp = new PdfFileStamp();
            pdfStamp.BindPdf(inputPath);
            pdfStamp.AddStamp(stamp);
            pdfStamp.Save(outputPath);

            Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
