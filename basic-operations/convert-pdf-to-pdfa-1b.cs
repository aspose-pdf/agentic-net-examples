using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // for tagged content if needed (not required for conversion)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set up conversion options for PDF/A‑1b
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Preserve embedded fonts and structure (default behavior)
            options.OptimizeFileSize = false; // do not down‑size fonts
            // Perform the conversion
            bool success = doc.Convert(options);
            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/A‑1b failed.");
                return;
            }

            // Save the converted document as PDF (PDF/A‑1b)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}