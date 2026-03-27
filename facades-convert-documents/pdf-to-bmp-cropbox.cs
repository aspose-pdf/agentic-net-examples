using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "page_{0}_out.bmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            Resolution resolution = new Resolution(300);
            // In recent Aspose.Pdf versions CropBox is the default coordinate type for BmpDevice.
            // Therefore we do not set the CoordinateType property (it no longer exists).
            BmpDevice bmpDevice = new BmpDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = string.Format(outputPattern, pageNumber);
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("Conversion to BMP completed.");
    }
}
