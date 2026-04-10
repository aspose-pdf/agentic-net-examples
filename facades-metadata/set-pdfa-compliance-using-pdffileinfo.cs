using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF, convert it to PDF/A‑1b, then enforce strict validation via PdfFileInfo.
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/A‑1b (archival format). Errors are logged to the specified file.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // PdfFileInfo works on the already‑converted document.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(doc))
            {
                // Enable strict validation to ensure PDF/A compliance.
                pdfInfo.UseStrictValidation = true;

                // Save the updated PDF/A‑compliant document.
                pdfInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"PDF/A‑compliant file saved to '{outputPath}'.");
    }
}