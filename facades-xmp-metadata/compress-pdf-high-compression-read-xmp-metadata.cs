using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string compressedPdf = "compressed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF, apply high compression, and save the result
        using (Document doc = new Document(inputPdf))
        {
            // Configure aggressive optimization options
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true,          // Pack objects into streams
                RemoveUnusedObjects = true,      // Drop objects not referenced by any page
                LinkDuplicateStreams = true      // Merge identical streams
            };

            // ImageCompressionOptions is read‑only; modify the existing instance
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 90; // 0‑100 (higher = better quality)
            opt.ImageCompressionOptions.Encoding = ImageEncoding.Flate; // ZIP compression

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(compressedPdf);
        }

        // Read XMP metadata from the compressed PDF using the Facade API
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(compressedPdf);
            byte[] rawMetadata = xmp.GetXmpMetadata(); // XML bytes
            string xml = System.Text.Encoding.UTF8.GetString(rawMetadata);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xml);
        }
    }
}