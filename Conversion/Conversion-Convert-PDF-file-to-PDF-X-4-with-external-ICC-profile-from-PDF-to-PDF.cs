using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";       // source PDF
        const string outputPath     = "output_pdfx4.pdf";// destination PDF/X‑4
        const string iccProfilePath = "profile.icc";     // external ICC profile

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion options:
                //   - Target format: PDF/X‑4
                //   - Error handling: delete objects that cannot be converted
                //   - External ICC profile for color management
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete)
                {
                    IccProfileFileName = iccProfilePath
                };

                // Perform the conversion according to the options
                doc.Convert(options);

                // Save the converted document to the desired output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X‑4: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}