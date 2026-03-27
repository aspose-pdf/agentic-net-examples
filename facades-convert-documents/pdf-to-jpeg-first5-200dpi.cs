using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            Resolution resolution = new Resolution(200);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            int pageLimit = Math.Min(5, pdfDocument.Pages.Count);
            for (int pageNumber = 1; pageNumber <= pageLimit; pageNumber++)
            {
                string outputFile = $"image{pageNumber}_out.jpeg";
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}