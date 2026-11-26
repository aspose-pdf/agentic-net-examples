using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "documentD.pdf";
        const string iccProfilePath = "profileG.icc";
        const string outputPdfPath = "documentD_PDFX3.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare conversion options for PDF/X-3:2003 compliance
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

                // Embed the external ICC profile – use IccProfileFileName (or OutputIntent) property
                convOptions.IccProfileFileName = iccProfilePath;

                // Perform the conversion
                doc.Convert(convOptions);

                // Save the converted PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/X-3 compliant file saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
