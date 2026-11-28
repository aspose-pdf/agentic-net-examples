using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string boundPdf = "bound.pdf";
        const string outputPng = "page1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind (linearize) the PDF using PdfFileMend
        var mend = new PdfFileMend();
        mend.BindPdf(inputPdf);
        mend.Save(boundPdf);

        // Load the bound PDF
        using (var pdfDoc = new Document(boundPdf))
        {
            // Image devices (e.g., PngDevice) are concrete but do NOT implement IDisposable.
            // Therefore they must NOT be placed inside a using block.
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true // optional – makes background transparent
            };

            // Convert the first page to PNG. The Process method can write directly to a file path.
            pngDevice.Process(pdfDoc.Pages[1], outputPng);
        }

        // Remove the temporary bound PDF
        try { File.Delete(boundPdf); } catch { }

        Console.WriteLine($"Page 1 saved as PNG: {outputPng}");
    }
}
