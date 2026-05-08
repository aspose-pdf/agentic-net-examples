using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string compressedPath = "compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and apply high compression using OptimizationOptions
        using (Document doc = new Document(inputPath))
        {
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressImages = true;
            opt.ImageQuality = 50; // adjust as needed for higher compression
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;

            doc.OptimizeResources(opt);
            doc.Save(compressedPath);
        }

        // Read XMP metadata from the compressed PDF using the Facades API
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(compressedPath);
            byte[] data = xmp.GetXmpMetadata();               // Get metadata as XML bytes
            string xml = System.Text.Encoding.UTF8.GetString(data);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xml);
        }
    }
}