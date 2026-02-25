using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texFilePath   = "input.tex";          // source TeX file
        const string pdfEFilePath  = "output_pdfe.pdf";   // target PDF/E file
        const string logFilePath   = "conversion_log.txt"; // optional log for conversion issues

        // Verify source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX source not found: {texFilePath}");
            return;
        }

        try
        {
            // Load the TeX file into a PDF document using TeXLoadOptions
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();
            using (Document pdfDoc = new Document(texFilePath, texLoadOptions))
            {
                // Prepare conversion options to PDF/E (engineering) format.
                // PdfFormat.PDF_E_1 is the enum value for PDF/E‑1.
                // ConvertErrorAction.Delete tells the engine to drop objects it cannot convert.
                PdfFormatConversionOptions convOptions =
                    new PdfFormatConversionOptions(logFilePath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Perform the conversion. The method returns true on success.
                bool converted = pdfDoc.Convert(convOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion reported failures – see log for details.");
                }

                // Save the resulting PDF/E document.
                pdfDoc.Save(pdfEFilePath);
                Console.WriteLine($"Successfully saved PDF/E file to '{pdfEFilePath}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}