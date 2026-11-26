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
        const string validationReport = "validation_report.xml";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Register a simple font substitution (fallback to Arial for any missing font)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

            using (Document doc = new Document(inputPath))
            {
                // Validate the PDF (writes an XML report and returns whether the document is valid)
                bool isValid = doc.Validate(validationReport, PdfFormat.PDF_A_3B);
                // Optional: inspect isValid or the generated report

                // Convert to PDF/A‑3b. The overload requires a log file path and an error‑handling action.
                // ConvertErrorAction.Delete is the most widely supported option for older Aspose.PDF versions.
                doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3b saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
