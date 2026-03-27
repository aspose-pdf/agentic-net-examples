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

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Use a resolution of 72 DPI to keep the original page dimensions (1 point = 1/72 inch)
            Resolution resolution = new Resolution(72);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"page{pageNumber}.jpeg";
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputFile}");
            }
        }
    }
}