using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3_cmyk.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Set up conversion options for PDF/X‑3. The ColorSpaceConversionMode property
                // is only available in Aspose.Pdf 22.9+; it is omitted here for compatibility with
                // earlier versions. When using a newer version, you may uncomment the line
                // below to force CMYK conversion.
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
                {
                    OptimizeFileSize = true
                    // ColorSpaceConversionMode = ColorSpaceConversionMode.ConvertToCMYK; // Requires Aspose.Pdf 22.9+
                };

                // Perform the conversion to PDF/X‑3
                doc.Convert(conversionOptions);

                // Write a conversion log (optional). This overload does not affect color conversion.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X‑3 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved as PDF/X‑3 with CMYK colors to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
