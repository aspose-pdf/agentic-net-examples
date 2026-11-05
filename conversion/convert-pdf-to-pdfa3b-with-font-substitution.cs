using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Register font substitutions for missing fonts
                // Substitute Helvetica with Arial (add more mappings as required)
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
                // Example of a wildcard substitution for any missing font
                // FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

                // Convert the document to PDF/A‑3b; errors are logged to the specified XML file
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑3b document
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
