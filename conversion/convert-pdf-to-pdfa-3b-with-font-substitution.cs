using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for SimpleFontSubstitution and ConvertErrorAction

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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Configure simple font substitution for any missing fonts.
            // This maps a missing font name to a substitute font that exists
            // on the system (e.g., substitute with Arial).
            // ------------------------------------------------------------
            // Font substitution is performed via the static FontRepository.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

            // ------------------------------------------------------------
            // Convert the document to PDF/A‑3b.
            // Use the core Document.Convert method – it writes a log file
            // and performs the conversion in one step.
            // ------------------------------------------------------------
            doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // ------------------------------------------------------------
            // Save the resulting PDF/A‑3b document.
            // Document.Save(string) writes a PDF regardless of the file extension.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
    }
}
