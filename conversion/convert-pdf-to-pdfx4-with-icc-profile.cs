using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "documentB.pdf";
        const string iccPath    = "profileA.icc";
        const string outputPath = "documentB_pdfx4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion to PDF/X-4 with the specified ICC profile
                var options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccPath
                };

                // Perform the conversion
                doc.Convert(options);

                // Save the resulting PDF/X-4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
