using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XPS file, output PDF/E file and a conversion log.
        const string xpsPath   = "input.xps";
        const string pdfPath   = "output.pdf";
        const string logPath   = "conversion.log";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        try
        {
            // Load the XPS document using XpsLoadOptions.
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(
                       xpsPath,
                       new Aspose.Pdf.XpsLoadOptions()))
            {
                // Convert to PDF/E (engineered PDF) format.
                // PDF/E is based on PDF/X‑4, so we use PdfFormat.PDF_X_4.
                pdfDoc.Convert(
                    logPath,
                    Aspose.Pdf.PdfFormat.PDF_X_4,
                    Aspose.Pdf.ConvertErrorAction.Delete);

                // Save the converted document as a PDF file.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"Conversion completed. PDF/E saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}