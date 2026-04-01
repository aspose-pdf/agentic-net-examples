using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);
            byte[] data = xmp.GetXmpMetadata();
            string xml = Encoding.UTF8.GetString(data);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xml);
        }
    }
}