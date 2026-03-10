using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xslFoPath      = "input.xslfo";   // XSL‑FO source file
        const string iccProfilePath = "profile.icc";   // External ICC profile
        const string outputPdfPath  = "output_pdfx4.pdf";

        // Verify input files exist
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

        // Load the XSL‑FO document using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(xslFoPath);
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Prepare conversion options for PDF/X‑4 and attach the ICC profile
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convOptions.IccProfileFileName = iccProfilePath;

            // Perform the conversion; Convert returns true on success
            bool success = pdfDoc.Convert(convOptions);
            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/X‑4 failed.");
                return;
            }

            // Save the resulting PDF/X‑4 document
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF/X‑4 file saved to '{outputPdfPath}'.");
        }
    }
}