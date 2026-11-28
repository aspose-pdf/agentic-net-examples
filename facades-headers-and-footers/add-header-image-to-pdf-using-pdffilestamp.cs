using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string headerImagePath = "header1.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Create a PdfFileStamp instance.
        PdfFileStamp stamper = new PdfFileStamp();

        // Bind the source PDF.
        stamper.BindPdf(inputPdf);

        // Add the header image using a FileStream with a top margin of 6.
        using (FileStream imgStream = File.OpenRead(headerImagePath))
        {
            stamper.AddHeader(imgStream, 6);
        }

        // Save the stamped PDF.
        stamper.Save(outputPdf);

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}