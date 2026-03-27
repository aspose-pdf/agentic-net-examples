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
            // Set resolution to 300 DPI
            Resolution resolution = new Resolution(300);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            int startPage = 1;
            int endPage = 3;
            int totalPages = pdfDocument.Pages.Count;
            if (endPage > totalPages)
            {
                endPage = totalPages;
            }

            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string outputFile = $"image{pageNumber}_out.jpeg";
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
            }
        }

        Console.WriteLine("JPEG preview images created.");
    }
}