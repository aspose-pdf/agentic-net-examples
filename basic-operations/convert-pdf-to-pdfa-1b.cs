using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion to PDF/A‑1b
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                // Perform the conversion
                doc.Convert(options);
                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}