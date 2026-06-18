using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace PdfToJpegExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Load the PDF document
            using (Document pdfDocument = new Document("input.pdf"))
            {
                // Set resolution to 200 DPI
                Resolution resolution = new Resolution(200);
                JpegDevice jpegDevice = new JpegDevice(resolution);

                int pageCount = pdfDocument.Pages.Count;
                int maxPages = Math.Min(5, pageCount);
                for (int pageNumber = 1; pageNumber <= maxPages; pageNumber++)
                {
                    string outputFile = $"page{pageNumber}.jpeg";
                    using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }
        }
    }
}
