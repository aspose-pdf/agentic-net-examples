using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Add custom font substitution rules (fallback fonts)
            // Example: replace missing "TimesNewRomanPSMT" with "Arial"
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial", false));
            // Add additional substitutions as needed, e.g.:
            // FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "LiberationSans", false));

            // Configure PDF/A conversion options
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Enable default substitution strategy for fonts that cannot be embedded
            conversionOptions.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Perform the conversion to PDF/A
            doc.Convert(conversionOptions);

            // Save the converted document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed: {outputPath}");
    }
}