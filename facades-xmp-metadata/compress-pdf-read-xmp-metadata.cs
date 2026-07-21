using System;
using System.IO;
using System.Text;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF and apply high compression using the current OptimizationOptions API.
        using (Document doc = new Document(inputPdf))
        {
            // Create a base set of optimization options (all recommended defaults).
            OptimizationOptions opt = OptimizationOptions.All();

            // Image compression is now configured via the existing ImageCompressionOptions instance.
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 30; // lower quality = higher compression

            // Keep the most common clean‑up flags that are still available.
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;

            // Apply the optimization settings.
            doc.OptimizeResources(opt);

            // Save the compressed PDF.
            doc.Save(compressedPdf);
        }

        // Read XMP metadata from the compressed PDF using the Facades API.
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(compressedPdf);
        byte[] xmpData = xmp.GetXmpMetadata();

        // Convert the XML bytes to a string for display.
        string xmpXml = Encoding.UTF8.GetString(xmpData);
        Console.WriteLine("XMP Metadata (XML):");
        Console.WriteLine(xmpXml);
    }
}
