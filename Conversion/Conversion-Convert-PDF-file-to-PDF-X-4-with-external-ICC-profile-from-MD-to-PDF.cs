using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string outputPdfPath  = "output_pdfx4.pdf"; // destination PDF/X-4
        const string iccProfilePath = "profile.icc";    // external ICC profile

        // Verify required files exist
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
            // Load source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Configure conversion options: target PDF/X-4 format with external ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath
                };

                // Perform the conversion; Convert returns true on success
                bool converted = doc.Convert(convOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/X-4 failed.");
                }

                // Save the converted document as a regular PDF file
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully converted to PDF/X-4: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}