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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the PDF, apply high compression, and save the result
        using (Document document = new Document(inputPath))
        {
            // Apply high compression using OptimizationOptions. This works for all
            // Aspose.Pdf versions, even when the Document.Compress() method is not
            // available.
            OptimizationOptions opt = OptimizationOptions.All();
            // Increase compression aggressiveness (you can tune these values).
            opt.CompressImages = true;
            opt.ImageQuality = 50; // lower quality → higher compression
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            document.OptimizeResources(opt);

            document.Save(compressedPath);
        }

        // Read XMP metadata from the compressed PDF
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        xmpMetadata.BindPdf(compressedPath);
        byte[] metadataBytes = xmpMetadata.GetXmpMetadata();
        string metadataXml = System.Text.Encoding.UTF8.GetString(metadataBytes);
        Console.WriteLine("XMP Metadata:");
        Console.WriteLine(metadataXml);
    }
}
