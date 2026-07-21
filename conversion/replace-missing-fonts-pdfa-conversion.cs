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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Configure PDF/A conversion options
            // -----------------------------------------------------------------
            // Convert to PDF/A‑1B (you can choose another PDF/A level if required)
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                // Enable default font substitution when embedding fails
                FontEmbeddingOptions = { UseDefaultSubstitution = true },

                // Optional: reduce file size by subsetting fonts
                OptimizeFileSize = true
            };

            // -----------------------------------------------------------------
            // Register fallback font substitutions
            // -----------------------------------------------------------------
            // Example: replace any missing "TimesNewRomanPSMT" with "Arial"
            // Add as many SimpleFontSubstitution entries as needed.
            FontRepository.Substitutions.Add(
                new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial", false));

            // Example: replace any missing "Helvetica" with "LiberationSans"
            FontRepository.Substitutions.Add(
                new SimpleFontSubstitution("Helvetica", "LiberationSans", false));

            // -----------------------------------------------------------------
            // Perform the conversion
            // -----------------------------------------------------------------
            doc.Convert(convOpts);

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}