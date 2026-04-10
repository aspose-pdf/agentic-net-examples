using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;
using Aspose.Pdf.Facades;

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

        // Load the PDF and apply high compression settings
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true,
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true,
                LinkDuplicateStreams = true
            };

            // ImageCompressionOptions is read‑only; modify its existing instance
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 30; // lower quality = higher compression
            opt.ImageCompressionOptions.Encoding = ImageEncoding.Flate; // use Flate compression

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(compressedPath);
        }

        // Read XMP metadata from the compressed PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(compressedPath);
            byte[] data = xmp.GetXmpMetadata(); // XML bytes
            string xml = System.Text.Encoding.UTF8.GetString(data);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xml);
        }
    }
}