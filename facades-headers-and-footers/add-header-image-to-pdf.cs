using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string bannerPath = "banner.jpg";
        const int    topMargin  = 30; // distance from the top edge in points

        // Verify that required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bannerPath))
        {
            Console.Error.WriteLine($"Header image not found: {bannerPath}");
            return;
        }

        // PdfFileStamp implements IDisposable, so wrap it in a using block.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            // Bind the source PDF to the stamp object.
            stamp.BindPdf(inputPdf);

            // Open the banner image as a FileStream and add it as a header.
            using (FileStream imgStream = File.OpenRead(bannerPath))
            {
                // AddHeader takes the image stream and the top margin (in points).
                stamp.AddHeader(imgStream, topMargin);
            }

            // Save the stamped PDF to the desired output path.
            stamp.Save(outputPdf);
        }

        Console.WriteLine($"Header image added and saved to '{outputPdf}'.");
    }
}