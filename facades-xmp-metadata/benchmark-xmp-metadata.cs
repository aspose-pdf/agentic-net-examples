using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(document);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            byte[] xmpData = xmpMetadata.GetXmpMetadata();
            timer.Stop();

            Console.WriteLine("XMP metadata size (bytes): " + xmpData.Length);
            Console.WriteLine("Time elapsed (ms): " + timer.ElapsedMilliseconds);
        }
    }
}