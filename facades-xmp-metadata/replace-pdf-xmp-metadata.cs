using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmpFile = "metadata.xmp";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xmpFile))
        {
            Console.Error.WriteLine($"XMP file not found: {xmpFile}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            using (FileStream xmpStream = new FileStream(xmpFile, FileMode.Open, FileAccess.Read))
            {
                pdfDocument.SetXmpMetadata(xmpStream);
            }

            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"XMP metadata replaced and saved to '{outputPdf}'.");
    }
}