using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;               // FontRepository (optional font substitution)
using Aspose.Pdf.Devices;            // Resolution and image conversion helpers

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string fontsPath = "fonts";          // folder containing custom fonts (optional)
        const string outputDir = "GifPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // OPTIONAL: register custom fonts or add font substitutions.
            // The AddFontFolder method is not available in recent versions of Aspose.Pdf.
            // If you need to substitute missing fonts you can use the Substitutions collection:
            // if (Directory.Exists(fontsPath))
            //     FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

            // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF
                converter.BindPdf(inputPdf);

                // Set conversion range and resolution
                converter.StartPage   = 2;   // start from page 2
                converter.EndPage     = 3;   // end at page 3
                converter.Resolution  = new Resolution(72); // DPI

                // Perform the conversion – this prepares the internal image stream
                converter.DoConvert();

                // Save each page in the selected range as a GIF image.
                // GetNextImage infers the image format from the file extension.
                for (int page = converter.StartPage; page <= converter.EndPage; page++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{page}.gif");
                    converter.GetNextImage(outPath);
                    Console.WriteLine($"Page {page} saved as GIF: {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
