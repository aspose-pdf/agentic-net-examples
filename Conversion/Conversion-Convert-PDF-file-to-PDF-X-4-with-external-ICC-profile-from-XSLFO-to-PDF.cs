using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use a generic data directory that can be placed next to the executable
        string dataDir = "data";
        string xslFoPath = Path.Combine(dataDir, "input.fo");          // XSL‑FO source
        string iccProfilePath = Path.Combine(dataDir, "profile.icc"); // External ICC profile
        string outputPdfPath = Path.Combine(dataDir, "output_pdfx4.pdf"); // PDF/X‑4 result

        // Verify required files exist
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load XSL‑FO and create a PDF document
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Set conversion options to PDF/X‑4 and attach the ICC profile
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath
                };

                // Convert the document to PDF/X‑4 first
                pdfDocument.Convert(conversionOptions);

                // Then save the converted document as a regular PDF file
                pdfDocument.Save(outputPdfPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"PDF/X‑4 file created successfully at: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
