using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtInputPath   = "input.mht";
        const string pdfOutputPath  = "output_pdfx4.pdf";
        const string iccProfilePath = "sRGB.icc";

        // Verify files exist
        if (!File.Exists(mhtInputPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtInputPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the MHT file into a PDF document
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtInputPath, loadOptions))
        {
            // Prepare conversion options for PDF/X‑4 and attach the external ICC profile
            PdfFormatConversionOptions convertOpts = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convertOpts.IccProfileFileName = iccProfilePath;

            // Perform the conversion
            pdfDoc.Convert(convertOpts);

            // Save the resulting PDF/X‑4 document
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑4 saved to '{pdfOutputPath}'.");
    }
}