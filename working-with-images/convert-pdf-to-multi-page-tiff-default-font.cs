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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set a default font substitution for missing fonts.
                // Map common PDF base fonts to Arial (or any font that exists on the system).
                Font defaultFont = FontRepository.FindFont("Arial");
                if (defaultFont != null)
                {
                    FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", defaultFont.FontName));
                    FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times-Roman", defaultFont.FontName));
                    FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", defaultFont.FontName));
                }

                // Configure TIFF settings – use CCITT Group 4 compression (optional)
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.CCITT4,
                    // You can adjust other settings such as Depth, Shape, SkipBlankPages, etc.
                };

                // Create a TiffDevice with desired resolution (e.g., 300 DPI) and the settings above
                TiffDevice tiffDevice = new TiffDevice(new Resolution(300), tiffSettings);

                // Apply rendering options – set the default font name for rendering
                tiffDevice.RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Arial"
                };

                // Process the whole document (from first to last page) and save as a multi‑page TIFF
                tiffDevice.Process(doc, 1, doc.Pages.Count, outputPath);
            }

            Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
