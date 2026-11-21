using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Apply font substitution: replace Symbol with Arial Unicode MS
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

            // Define the resolution (DPI) for rasterization
            Resolution resolution = new Resolution(150);

            // Optional TIFF settings (e.g., compression)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW
            };

            // Create a TiffDevice (it does NOT implement IDisposable, so no using block)
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Render all pages of the PDF into a single multi‑page TIFF file
            using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
            }

            // Dispose the document (Document implements IDisposable)
            pdfDoc.Dispose();

            Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
