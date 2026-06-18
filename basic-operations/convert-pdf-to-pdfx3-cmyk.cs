using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string iccProfilePath = "CMYK.icc"; // Path to a CMYK ICC profile

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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/X‑3 compliance
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);
            // Force all color spaces to CMYK by specifying a CMYK ICC profile
            options.IccProfileFileName = iccProfilePath;

            // Convert the document to PDF/X‑3 using the specified options
            doc.Convert(options);

            // Save the converted document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 compliant file saved to '{outputPath}'.");
    }
}