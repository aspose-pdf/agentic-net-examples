using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";          // source XML file
        const string iccPath   = "profile.icc";        // external ICC profile
        const string outputPdf = "output_pdfx4.pdf";   // destination PDF/X‑4 file
        const string logPath   = "conversion_log.txt"; // optional log file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        try
        {
            // Load the XML representation of a PDF document.
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Configure conversion to PDF/X‑4 and attach the external ICC profile.
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convOptions.IccProfileFileName = iccPath;                 // external ICC profile
                convOptions.ErrorAction      = ConvertErrorAction.Delete; // handle conversion errors

                // Perform the conversion.
                doc.Convert(convOptions);

                // Save the resulting PDF/X‑4 document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion succeeded. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}