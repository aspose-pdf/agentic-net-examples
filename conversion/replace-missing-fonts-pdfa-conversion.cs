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
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // ---------------------------------------------------------------
            // Configure font substitution: replace missing fonts with fallback fonts
            // ---------------------------------------------------------------
            // Example: if the source PDF uses "MissingFont", replace it with "Arial"
            FontRepository.Substitutions.Add(
                new SimpleFontSubstitution("MissingFont", "Arial"));

            // ---------------------------------------------------------------
            // Convert to PDF/A-1B (PDF/A compliance) using Document.Convert overload
            // ---------------------------------------------------------------
            // The Convert method creates a conversion log and applies the required PDF/A rules.
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}
