using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlInputPath   = "input.xml";          // source XML file
        const string iccProfilePath = "profile.icc";        // external ICC profile
        const string pdfOutputPath  = "output_pdfx4.pdf";   // resulting PDF/X‑4 file

        // Verify input files exist
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML input not found: {xmlInputPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the XML document using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlInputPath, loadOptions))
            {
                // Prepare conversion options for PDF/X‑4 and attach the ICC profile
                PdfFormatConversionOptions convertOpts = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath
                };

                // Perform the conversion
                bool conversionResult = doc.Convert(convertOpts);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion to PDF/X‑4 failed.");
                    return;
                }

                // Save the resulting PDF (default Save writes PDF)
                doc.Save(pdfOutputPath);
                Console.WriteLine($"Successfully saved PDF/X‑4 to '{pdfOutputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}