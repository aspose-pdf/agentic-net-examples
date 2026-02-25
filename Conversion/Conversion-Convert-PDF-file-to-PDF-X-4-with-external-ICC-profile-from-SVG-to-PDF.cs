using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string svgPath        = "input.svg";          // Source SVG file
        const string iccProfilePath = "profile.icc";        // External ICC profile
        const string outputPdfPath  = "output_pdfx4.pdf";   // Result PDF/X‑4 file
        const string conversionLog  = "conversion_log.txt"; // Optional log file

        // Verify input files exist
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the SVG using SvgLoadOptions.
            // The NewEngine provides the latest conversion quality.
            var loadOptions = new SvgLoadOptions
            {
                ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
            };

            // Load the SVG into a Document (PDF in memory).
            using (Document doc = new Document(svgPath, loadOptions))
            {
                // Prepare conversion options for PDF/X‑4.
                var convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete)
                {
                    // Attach the external ICC profile.
                    IccProfileFileName = iccProfilePath,
                    // Optional: store conversion messages.
                    LogFileName = conversionLog
                };

                // Perform the conversion to PDF/X‑4.
                bool success = doc.Convert(convertOptions);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failures. Check the log file for details.");
                }

                // Save the resulting PDF/X‑4 document.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"SVG successfully converted to PDF/X‑4: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}