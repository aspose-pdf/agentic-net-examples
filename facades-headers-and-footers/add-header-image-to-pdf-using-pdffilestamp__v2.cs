using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string headerImagePath = "header2.tiff";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // PdfFileStamp does not implement IDisposable, so it is not wrapped in a using block.
        PdfFileStamp pdfStamp = new PdfFileStamp();

        // Bind the source PDF document.
        pdfStamp.BindPdf(inputPdfPath);

        // Add the header image using a FileStream with a top margin of 6 units.
        using (FileStream headerStream = File.OpenRead(headerImagePath))
        {
            pdfStamp.AddHeader(headerStream, 6);
        }

        // Save the stamped PDF to the output file.
        pdfStamp.Save(outputPdfPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}