using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Input XML file that defines the PDF content (e.g., XSL‑FO or other supported XML)
        const string xmlInputPath = "input.xml";
        // Output PDF file
        const string pdfOutputPath = "optimized_output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Create a new empty PDF document
            using (Document pdfDoc = new Document())
            {
                // Load the XML content into the document (BindXml is the correct method)
                pdfDoc.BindXml(xmlInputPath);

                // Configure overall optimization options
                OptimizationOptions opt = new OptimizationOptions
                {
                    CompressObjects = true,
                    RemoveUnusedObjects = true,
                    RemoveUnusedStreams = true,
                    SubsetFonts = true
                };

                // Configure image‑compression options (downsample large images)
                // ImageCompressionOptions is a read‑only property of OptimizationOptions;
                // we modify its members directly.
                opt.ImageCompressionOptions.MaxResolution = 150; // Downsample images that exceed this DPI value
                // The CompressionMethod property is not available in the current Aspose.Pdf version;
                // JPEG compression is the default when ImageQuality is set, so we omit it.
                opt.ImageCompressionOptions.ImageQuality = 80; // Quality for JPEG compression (0‑100)

                // Apply the optimization to the document
                pdfDoc.OptimizeResources(opt);

                // Save the optimized PDF
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
