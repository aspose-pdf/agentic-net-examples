using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options targeting PDF/A‑3b format
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);

            // Enable substitution of missing fonts with default substitutes
            options.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Optional: specify a log file to capture conversion messages
            options.LogFileName = "conversion.log";

            // Perform the conversion; the document is now PDF/A‑3b compliant
            doc.Convert(options);

            // Save the converted document; the output will be a PDF/A‑3b file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
    }
}