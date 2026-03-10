using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx4.pdf";
        const string iccProfilePath = "profile.icc";

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
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion to PDF/X-4 with external ICC profile
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                options.IccProfileFileName = iccProfilePath;

                // Perform the conversion
                bool converted = doc.Convert(options);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/X-4 failed.");
                }

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X-4: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}