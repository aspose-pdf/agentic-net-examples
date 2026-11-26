using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Document implements IDisposable, so we keep it in a using block.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // JpegDevice does NOT implement IDisposable; instantiate it directly.
            // 72 DPI keeps 1 point = 1 pixel, preserving the original page size.
            JpegDevice jpegDevice = new JpegDevice(72);

            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // The page already retains its original dimensions, but the code can
                // explicitly read them if needed:
                // double originalWidth = page.PageInfo.Width;
                // double originalHeight = page.PageInfo.Height;

                string outputPath = Path.Combine(outputFolder, $"Page_{i}.jpg");
                jpegDevice.Process(page, outputPath);
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images.");
    }
}
