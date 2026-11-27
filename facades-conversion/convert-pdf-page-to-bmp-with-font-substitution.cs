using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputBmp = "page2.bmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Configure fallback font substitutions
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times-Roman", "Times New Roman"));

        // Load the PDF document (PdfConverter is not required for image rendering)
        using (var pdfDocument = new Document(inputPdf))
        {
            const int pageNumber = 2;
            if (pdfDocument.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"PDF does not contain page {pageNumber}.");
                return;
            }

            // Render the selected page to BMP using BmpDevice.
            // Image devices are concrete but do NOT implement IDisposable, so do NOT wrap them in a using block.
            var resolution = new Resolution(300); // 300 DPI – adjust as needed
            var bmpDevice = new BmpDevice(resolution);
            bmpDevice.Process(pdfDocument.Pages[pageNumber], outputBmp);
        }

        Console.WriteLine($"Page {2} saved as BMP to '{outputBmp}'.");
    }
}
