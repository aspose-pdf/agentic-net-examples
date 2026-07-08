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
        const string outputPdf = "compressed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF, apply high‑level compression, and save the result.
        using (Document doc = new Document(inputPdf))
        {
            // Use OptimizationOptions for compression – Document.Compress() does not exist.
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressImages = true;          // compress embedded images
            opt.ImageQuality = 50;              // quality level (0‑100)
            opt.RemoveUnusedObjects = true;    // clean up unused objects
            opt.RemoveUnusedStreams = true;    // clean up unused streams
            // You can also enable other options such as opt.RemoveUnusedFonts = true;

            doc.OptimizeResources(opt);

            // Save the compressed PDF.
            doc.Save(outputPdf);
        }

        // Read XMP metadata from the compressed PDF using the Facades API.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(outputPdf);
            byte[] data = xmp.GetXmpMetadata();

            // Convert the XML bytes to a readable string.
            string xml = Encoding.UTF8.GetString(data);
            Console.WriteLine("XMP Metadata (XML):");
            Console.WriteLine(xml);
        }
    }
}
