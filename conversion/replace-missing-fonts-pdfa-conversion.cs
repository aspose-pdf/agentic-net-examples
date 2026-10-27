using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for SimpleFontSubstitution

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

        // Map missing fonts to fallback fonts using FontRepository.
        // Example mappings; add as needed for your documents.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        // If you want a generic fallback for any other missing font, you can add a wildcard substitution.
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure PDF/A conversion options (PDF/A-1b in this example).
                PdfFormatConversionOptions opts = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                {
                    OptimizeFileSize = true
                    // Font substitution is already configured via FontRepository above.
                };

                // Perform the conversion.
                doc.Convert(opts);

                // Save the converted PDF/A document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
