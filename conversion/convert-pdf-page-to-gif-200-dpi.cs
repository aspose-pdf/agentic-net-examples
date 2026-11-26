using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string gifPath = "page4.gif";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfPath))
        {
            if (pdfDoc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The PDF does not contain a fourth page.");
                return;
            }

            Page page = pdfDoc.Pages[4];
            Resolution resolution = new Resolution(200);

            // GifDevice does NOT implement IDisposable, so it must NOT be wrapped in a using statement.
            GifDevice gifDevice = new GifDevice(resolution);

            using (FileStream imageStream = new FileStream(gifPath, FileMode.Create, FileAccess.Write))
            {
                gifDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Page 4 has been converted to GIF at 200 DPI: {gifPath}");
    }
}
