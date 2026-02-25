using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set up conversion options to PDF/A‑1B (you can change to PDF/X‑3 by using PdfFormat.PDF_X_3)
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                {
                    OptimizeFileSize = true // optional: reduces file size during conversion
                };

                // Perform the conversion
                doc.Convert(convOptions);

                // Save the converted document as a regular PDF file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}