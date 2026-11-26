using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Register a simple font substitution for missing fonts.
            // Missing "Times New Roman" will be replaced with "Arial".
            // SimpleFontSubstitution does not support named arguments, use positional parameters.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));

            // Convert the document to PDF/A‑1b. The Convert method writes a log file.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
