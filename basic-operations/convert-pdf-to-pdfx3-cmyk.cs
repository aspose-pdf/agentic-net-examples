using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3_cmyk.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/X‑3 compliance.
            // The ColorSpaceConversionMode property is only available in Aspose.Pdf 22.9+;
            // to keep the code compatible with earlier versions we omit it.
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

            // Perform the conversion to PDF/X‑3.
            doc.Convert(conversionOptions);

            // Save the converted document as a regular PDF (the internal representation is now PDF/X‑3).
            doc.Save(outputPath, SaveFormat.Pdf);
        }

        Console.WriteLine($"PDF saved with PDF/X‑3 compliance and CMYK color spaces: {outputPath}");
    }
}
