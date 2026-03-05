using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath      = "input.cgm";          // source CGM file
        const string iccProfile   = "profile.icc";        // external ICC profile
        const string outputPdf    = "output_pdfx4.pdf";   // resulting PDF/X‑4 file

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        if (!File.Exists(iccProfile))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfile}");
            return;
        }

        // Load CGM and convert to PDF/X‑4 using the specified ICC profile
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Prepare conversion options for PDF/X‑4
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convOpts.IccProfileFileName = iccProfile;   // external ICC profile

            // Perform the conversion
            doc.Convert(convOpts);

            // Save the resulting PDF/X‑4 document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Conversion completed: {outputPdf}");
    }
}