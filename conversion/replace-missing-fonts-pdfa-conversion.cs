using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // SimpleFontSubstitution and FontRepository reside here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Configure font substitution: map missing fonts to fallback fonts
            // ------------------------------------------------------------
            // Use FontRepository.Substitutions with SimpleFontSubstitution (FontSubstitution class has no public ctor)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial"));
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("CourierNewPSMT", "Courier New"));

            // ------------------------------------------------------------
            // Convert the document to PDF/A‑1b (PDF/A compliance)
            // ------------------------------------------------------------
            // The simple overload logs conversion details and creates PDF/A‑1b
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}
