using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set up conversion options for PDF/X‑3.
            // No ICC profile is specified, so the existing profiles in the source PDF are preserved.
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

            // Perform the conversion.
            bool success = doc.Convert(options);
            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/X‑3 failed.");
                return;
            }

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully converted to PDF/X‑3: {outputPath}");
    }
}