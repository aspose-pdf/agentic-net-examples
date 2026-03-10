using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output_pdfe1.pdf";
        const string logFilePath   = "conversion_log.xml";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Create a new Document instance
            using (Document doc = new Document())
            {
                // Load the CGM file using CgmLoadOptions
                doc.LoadFrom(inputCgmPath, new CgmLoadOptions());

                // Convert the loaded document to PDF/E-1 format
                // ConvertErrorAction.Delete skips objects that cannot be converted
                doc.Convert(logFilePath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the resulting PDF/E-1 document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed. PDF/E-1 saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}