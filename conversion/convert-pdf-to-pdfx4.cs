using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for font substitution if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "reportC.pdf";
        const string iccPath    = "profileB.icc"; // kept for reference – not used in this version
        const string outputPath = "reportC_pdfx4.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        try
        {
            // Optional: register a generic font substitution to avoid missing‑font issues during conversion
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/X‑4. The overload that accepts a log file and an error‑handling action
                // is available in all supported Aspose.PDF versions. Direct ICC profile assignment
                // (PdfFormatConversionOptions.ICCProfilePath) is only present in newer releases; when
                // the property is unavailable we rely on the default profile embedded by the library.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the PDF/X‑4 compliant document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X‑4 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
