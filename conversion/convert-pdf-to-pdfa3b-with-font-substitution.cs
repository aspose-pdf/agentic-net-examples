using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Register a fallback font for any missing font in the source PDF.
            // The wildcard "*" matches all missing fonts.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial Unicode MS"));

            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑3b. Errors are handled by deleting the offending objects.
                doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑3b file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
