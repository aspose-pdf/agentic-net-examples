using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for font substitution (optional)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath = "conversion_log.xml"; // log file for conversion details

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Optional: register a generic font substitution to avoid missing‑font errors
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

                // Convert the document to PDF/A‑3b.
                // The overload with a log file and ConvertErrorAction is the correct API.
                // ConvertErrorAction.Delete tells the converter to skip problematic elements.
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑3b and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
