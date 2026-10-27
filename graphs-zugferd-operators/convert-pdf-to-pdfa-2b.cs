using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa2b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑2b compliance
                PdfFormatConversionOptions opts = new PdfFormatConversionOptions(PdfFormat.PDF_A_2B)
                {
                    OptimizeFileSize = true
                };
                doc.Convert(opts);

                // Save the compliant PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with PDF/A‑2b compliance to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}