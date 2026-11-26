using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes (Document, PdfFormat, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "documentC.pdf";
        const string pdfEPath   = "documentC_PDFE1.pdf";
        const string reportPath = "validationReport.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF and convert it to PDF/E‑1.
            using (Document doc = new Document(inputPath))
            {
                // Optional conversion log for troubleshooting.
                string conversionLog = "conversionLog.xml";
                doc.Convert(conversionLog, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted PDF/E‑1 file.
                doc.Save(pdfEPath);
            }

            // Validate the resulting PDF/E‑1 file and generate a conformance report.
            using (Document pdfEDoc = new Document(pdfEPath))
            {
                // The Validate method writes the report to the specified path.
                pdfEDoc.Validate(reportPath, PdfFormat.PDF_E_1);
            }

            Console.WriteLine($"Conversion to PDF/E‑1 completed: {pdfEPath}");
            Console.WriteLine($"Validation report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
