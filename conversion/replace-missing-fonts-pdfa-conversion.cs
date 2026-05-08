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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure font substitution:
            // 1. Enable default substitution strategy (fallback to system fonts)
            // 2. Add a custom substitution rule for a specific missing font.
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            conversionOptions.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Example: replace any occurrence of "MissingFont" with "Arial"
            // The third parameter indicates whether the substitution is forced by a save option.
            SimpleFontSubstitution substitution = new SimpleFontSubstitution("MissingFont", "Arial", false);
            FontRepository.Substitutions.Add(substitution);

            // Perform the PDF/A conversion using the configured options
            doc.Convert(conversionOptions);

            // Save the converted document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}