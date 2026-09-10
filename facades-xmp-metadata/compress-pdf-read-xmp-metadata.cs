using System;
using System.IO;
using System.Text;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure high compression options
            OptimizationOptions opt = new OptimizationOptions
            {
                // Compress PDF objects (streams) into object streams
                CompressObjects = true,
                // Reuse identical page content when possible
                AllowReusePageContent = true,
                // Remove unused objects to reduce size
                RemoveUnusedObjects = true,
                // Subset fonts to keep only used glyphs
                SubsetFonts = true,
                // Unembed fonts if you prefer smaller size (optional)
                UnembedFonts = false,
                // Set maximum image resolution (typo in API: MaxResoultion)
                MaxResoultion = 72 // low resolution to shrink images further
            };

            // Apply optimization (compression)
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(compressedPath);
        }

        // Read XMP metadata from the compressed PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(compressedPath);
        byte[] rawMetadata = xmp.GetXmpMetadata();

        // Convert the metadata bytes to a readable string (UTF-8)
        string metadataXml = Encoding.UTF8.GetString(rawMetadata);
        Console.WriteLine("XMP Metadata (XML):");
        Console.WriteLine(metadataXml);
    }
}