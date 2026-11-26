using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional: provide a generic font substitution so missing fonts do not cause conversion failures
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑4. Use Delete as the closest behaviour to "skip" – it removes objects that cause errors.
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑4 file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
