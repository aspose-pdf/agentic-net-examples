using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "documentQ.pdf";
        const string outputPath = "documentQ_pdfx4.pdf";
        const string iccPath = "profileY.icc";

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
            // Load the source PDF
            Document doc = new Document(inputPath);

            // Set up conversion options for PDF/X‑4 and embed the ICC profile
            var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
            {
                IccProfileFileName = iccPath,
                OptimizeFileSize = true
            };

            // Perform the conversion to PDF/X‑4
            doc.Convert(convOptions);

            // Save the converted document as a regular PDF file (PDF/X‑4 is a PDF variant)
            doc.Save(outputPath, SaveFormat.Pdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}