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
        const string validationReportPath = "validation_report.xml";
        const string conversionLogPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Validate the PDF and write an XML validation report.
                //    The overload Validate(string, PdfFormat) writes the report and
                //    returns true when the document already conforms to the requested
                //    PDF/A format.
                // -----------------------------------------------------------------
                bool alreadyConforms = doc.Validate(validationReportPath, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Already PDF/A‑1b: {alreadyConforms}");

                // -----------------------------------------------------------------
                // 2. Register a simple font substitution – any missing font will be
                //    replaced with Arial.
                // -----------------------------------------------------------------
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

                // -----------------------------------------------------------------
                // 3. Convert to PDF/A‑1b.  Use ConvertErrorAction.Skip when the enum
                //    member exists; otherwise fall back to Delete (compatible with older
                //    Aspose.PDF versions).
                // -----------------------------------------------------------------
#if CONVERT_ERROR_ACTION_SKIP_AVAILABLE
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Skip);
#else
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
#endif

                // -----------------------------------------------------------------
                // 4. Save the converted PDF/A‑1b document.
                // -----------------------------------------------------------------
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
