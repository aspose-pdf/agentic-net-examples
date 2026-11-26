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
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Register a simple font substitution for any missing font (fallback to Arial)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑4 (PDF/A‑4B is the default compliance level)
                // The Convert method writes a log file and changes the document in‑place
                doc.Convert(conversionLog, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑4 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
