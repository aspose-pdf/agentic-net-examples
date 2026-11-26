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
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Apply simple font substitution to replace missing fonts.
            // Substitute any missing "Helvetica" font with "Arial" (non‑bold).
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial", false));

            // Enable auto‑tagging globally (static default instance)
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Convert the document to PDF/A‑1b, logging any conversion issues
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
