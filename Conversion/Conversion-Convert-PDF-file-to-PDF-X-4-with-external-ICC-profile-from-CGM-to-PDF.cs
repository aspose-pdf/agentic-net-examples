using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file, external ICC profile and output PDF/X-4 file paths
        const string inputCgmPath   = "input.cgm";
        const string iccProfilePath = "profile.icc";
        const string outputPdfPath  = "output_pdfx4.pdf";
        const string logPath        = "conversion_log.txt";

        // Verify that required files exist
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {inputCgmPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the CGM file into a PDF document using CgmLoadOptions
            using (Document doc = new Document())
            {
                CgmLoadOptions loadOptions = new CgmLoadOptions();
                doc.LoadFrom(inputCgmPath, loadOptions);

                // Prepare conversion options for PDF/X-4 with an external ICC profile
                PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convertOptions.IccProfileFileName = iccProfilePath;   // external ICC profile
                convertOptions.LogFileName       = logPath;           // optional log file

                // Perform the conversion to PDF/X-4
                bool conversionResult = doc.Convert(convertOptions);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion to PDF/X-4 failed. Check the log for details.");
                }

                // Save the resulting PDF/X-4 document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}