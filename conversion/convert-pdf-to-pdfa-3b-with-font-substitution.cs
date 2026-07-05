using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For PdfFormat enum (same namespace, but included for completeness)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF, perform conversion to PDF/A‑3b with font substitution,
        // then save the resulting PDF.
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/A‑3b.
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);

            // Enable default font substitution for fonts that cannot be embedded.
            convOptions.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Optional: reduce file size by optimizing fonts (does not affect substitution).
            // convOptions.OptimizeFileSize = true;

            // Perform the conversion.
            bool success = doc.Convert(convOptions);
            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/A‑3b failed.");
                return;
            }

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
    }
}