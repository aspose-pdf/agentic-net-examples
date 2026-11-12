using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

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

        try
        {
            // Load the source PDF
            Document doc = new Document(inputPath);

            // Configure high‑compression options (modern API)
            OptimizationOptions opt = OptimizationOptions.All();

            // ImageCompressionOptions is read‑only – configure the existing instance
            opt.ImageCompressionOptions.CompressImages = true;   // compress image streams
            opt.ImageCompressionOptions.ImageQuality = 30;       // lower quality → higher compression (0‑100)
            // Optional: choose an encoding (e.g., Flate) if you want explicit control
            // opt.ImageCompressionOptions.Encoding = ImageEncoding.Flate;

            opt.RemoveUnusedObjects = true;   // drop objects that are not referenced
            opt.RemoveUnusedStreams = true;   // drop streams that are not referenced
            opt.SubsetFonts = true;           // embed only used glyphs
            opt.LinkDuplicateStreams = true; // reuse identical streams

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(compressedPath, new PdfSaveOptions());

            // Load the compressed PDF and read its XMP metadata via the Metadata dictionary
            Document compressedDoc = new Document(compressedPath);
            var metadata = compressedDoc.Metadata;

            Console.WriteLine("XMP Metadata entries:");
            if (metadata != null && metadata.Count > 0)
            {
                foreach (var kvp in metadata)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            else
            {
                Console.WriteLine("(No XMP metadata found)");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
