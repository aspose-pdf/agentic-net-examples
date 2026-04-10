using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string iccProfilePath = "cmyk.icc"; // CMYK ICC profile file

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
                // Configure conversion to PDF/X‑3 and force CMYK color space via ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);
                convOptions.IccProfileFileName = iccProfilePath; // ensures CMYK conversion

                // Perform the conversion
                bool converted = doc.Convert(convOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/X‑3 failed.");
                    return;
                }

                // Save the resulting PDF/X‑3 document
                doc.Save(outputPath);
                Console.WriteLine($"PDF/X‑3 file saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}