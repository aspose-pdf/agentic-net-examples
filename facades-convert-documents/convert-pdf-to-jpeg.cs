using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            Resolution resolution = new Resolution(300);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"page{pageNumber}_out.jpeg";
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}