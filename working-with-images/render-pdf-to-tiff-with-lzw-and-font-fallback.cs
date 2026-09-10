using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;   // for font handling if needed

class Program
{
    static void Main()
    {
        // Paths – adjust as necessary
        string dataDir   = "Data";
        string pdfPath   = Path.Combine(dataDir, "input.pdf");
        string tiffPath  = Path.Combine(dataDir, "output.tif");

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // Default font fallback for missing characters.
            // PdfSaveOptions.DefaultFontName is used when a font is not
            // embedded in the source PDF. Saving to a memory stream with
            // these options forces the fallback to be applied, after which
            // the document can be rendered to TIFF.
            // ------------------------------------------------------------
            PdfSaveOptions fontFallbackOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial"   // any installed font can be used
            };

            using (MemoryStream fallbackStream = new MemoryStream())
            {
                // Save to memory with the fallback options (no file is created)
                pdfDocument.Save(fallbackStream, fontFallbackOptions);
                fallbackStream.Position = 0;

                // Reload the document so the fallback font is taken into account
                using (Document fallbackDoc = new Document(fallbackStream))
                {
                    // Create a resolution of 300 DPI for the TIFF output
                    Resolution resolution = new Resolution(300);

                    // Configure TIFF settings – LZW compression, default depth, portrait shape
                    TiffSettings tiffSettings = new TiffSettings
                    {
                        Compression = CompressionType.LZW,
                        Depth = ColorDepth.Default,
                        Shape = ShapeType.Portrait,
                        SkipBlankPages = false
                    };

                    // Initialise the TIFF device with the resolution and settings
                    TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                    // Render all pages of the PDF into a single multi‑page TIFF file
                    tiffDevice.Process(fallbackDoc, tiffPath);
                }
            }
        }

        Console.WriteLine($"TIFF image saved to: {tiffPath}");
    }
}