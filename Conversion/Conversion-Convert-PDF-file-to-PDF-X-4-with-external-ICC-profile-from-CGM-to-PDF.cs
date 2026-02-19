using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = "Data";
        string cgmPath = Path.Combine(dataDir, "input.cgm");
        string iccProfilePath = Path.Combine(dataDir, "profile.icc");
        string intermediatePdfPath = Path.Combine(dataDir, "intermediate.pdf");
        string finalPdfPath = Path.Combine(dataDir, "output_pdfx4.pdf");

        // Verify required files exist
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load CGM file into a PDF document and save an intermediate PDF
            var cgmLoadOptions = new CgmLoadOptions();
            using (var cgmPdf = new Document(cgmPath, cgmLoadOptions))
            {
                cgmPdf.Save(intermediatePdfPath);
            }

            // Load the intermediate PDF and convert it to PDF/X‑4 using the ICC profile
            using (var pdfDoc = new Document(intermediatePdfPath))
            {
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath
                };

                pdfDoc.Convert(conversionOptions);
                pdfDoc.Save(finalPdfPath);
            }

            Console.WriteLine($"PDF/X‑4 file created successfully: {finalPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
