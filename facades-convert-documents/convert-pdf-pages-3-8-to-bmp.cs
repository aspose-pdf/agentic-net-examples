using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document using the current Aspose.Pdf API (Document class)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Define the page range (inclusive)
            int startPage = 3;
            int endPage = 8;
            if (startPage < 1) startPage = 1;
            if (endPage > pdfDocument.Pages.Count) endPage = pdfDocument.Pages.Count;

            // Set the desired resolution for the bitmap output
            Resolution resolution = new Resolution(150); // 150 DPI – adjust as needed
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Convert each page in the range to a BMP image
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    // Process the specific page and write the BMP data to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine($"PDF pages {3}-{8} have been converted to BMP images.");
    }
}
