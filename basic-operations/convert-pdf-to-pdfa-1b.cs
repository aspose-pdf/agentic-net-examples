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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/A‑1b
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Preserve original structure and embedded fonts (default behavior)
            // Ensure no size‑optimisation that could drop fonts
            options.OptimizeFileSize = false;

            // Convert the document to PDF/A‑1b
            bool converted = doc.Convert(options);
            if (!converted)
            {
                Console.Error.WriteLine("Conversion to PDF/A‑1b failed.");
                return;
            }

            // Save the converted PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}