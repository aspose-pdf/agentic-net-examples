using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Paths for the original and the compressed PDF
        const string inputPdfPath = "input.pdf";
        const string compressedPdfPath = "compressed.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the original PDF
        Document doc = new Document(inputPdfPath);

        // Set high compression options using OptimizationOptions
        OptimizationOptions opt = OptimizationOptions.All();
        opt.CompressImages = true;          // compress image streams
        opt.ImageQuality = 30;              // reduce image quality (0‑100)
        opt.RemoveUnusedObjects = true;    // clean up unused objects
        opt.RemoveUnusedStreams = true;    // clean up unused streams

        // Apply the optimization and save the compressed PDF
        doc.OptimizeResources(opt);
        doc.Save(compressedPdfPath);

        // Load the compressed PDF and read its XMP metadata
        using (Document compressedDoc = new Document(compressedPdfPath))
        {
            // XMP metadata is exposed via the Metadata dictionary
            if (compressedDoc.Metadata != null && compressedDoc.Metadata.Count > 0)
            {
                Console.WriteLine("XMP Metadata after compression:");
                foreach (var kvp in compressedDoc.Metadata)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            else
            {
                Console.WriteLine("(No XMP metadata present)");
            }
        }
    }
}
