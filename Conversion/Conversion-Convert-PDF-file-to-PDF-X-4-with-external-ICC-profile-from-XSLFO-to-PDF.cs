using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xslFoPath      = "input.xslfo";      // Source XSL‑FO file
        const string iccProfilePath = "profile.icc";      // External ICC profile
        const string outputPath     = "output_pdfx4.pdf"; // Destination PDF/X‑4 file

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

        // Load the XSL‑FO document into a PDF Document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document doc = new Document(xslFoPath, loadOptions))
        {
            // Prepare conversion options for PDF/X‑4
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convOptions.IccProfileFileName = iccProfilePath;               // Attach external ICC profile
            convOptions.ErrorAction       = ConvertErrorAction.Delete;    // Handle conversion errors

            // Convert the in‑memory PDF to PDF/X‑4 using the specified options
            bool conversionOk = doc.Convert(convOptions);
            if (!conversionOk)
            {
                Console.Error.WriteLine("Conversion to PDF/X‑4 failed.");
                return;
            }

            // Save the resulting PDF/X‑4 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑4 file saved to '{outputPath}'.");
    }
}