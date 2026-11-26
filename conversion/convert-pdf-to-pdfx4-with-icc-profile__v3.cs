using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "manualR.pdf";
        const string outputPdf = "manualR_pdfx4.pdf";
        const string iccProfile = "profileAA.icc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(iccProfile))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfile}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Prepare conversion options for PDF/X‑4 and embed the ICC profile
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfile
                };

                // Perform the conversion
                doc.Convert(conversionOptions);

                // Save the resulting PDF/X‑4 document (SaveFormat.Pdf is sufficient after conversion)
                doc.Save(outputPdf, SaveFormat.Pdf);
            }

            Console.WriteLine($"PDF/X‑4 file created successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
