using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Register fallback font substitution: replace any missing font with Arial
        FontRepository.Substitutions.Add(
            new SimpleFontSubstitution("MissingFont", "Arial", false));

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/A
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Enable default substitution when a font cannot be embedded
            convOpts.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Perform PDF/A conversion with the configured options
            doc.Convert(convOpts);

            // Save the converted PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}