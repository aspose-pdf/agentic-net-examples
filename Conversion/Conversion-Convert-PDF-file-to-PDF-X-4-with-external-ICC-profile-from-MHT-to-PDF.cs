using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API (Document, PdfFormat, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtInputPath   = "input.mht";          // Source MHT file
        const string pdfOutputPath  = "output_pdfx4.pdf";   // Target PDF/X‑4 file
        const string iccProfilePath = "sRGB.icc";           // External ICC profile

        // Verify input files exist
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

        try
        {
            // Load the MHT file into a PDF document using MhtLoadOptions
            MhtLoadOptions loadOptions = new MhtLoadOptions();
            using (Document doc = new Document(mhtInputPath, loadOptions))
            {
                // -----------------------------------------------------------------
                // Convert the document to PDF/X‑4.
                // PdfFormatConversionOptions is used to specify the target format
                // and conversion behaviour.  An external ICC profile can be supplied
                // via the IccProfilePath property (available in recent Aspose.Pdf
                // versions).  Adjust the property name if your version uses a
                // different API.
                // -----------------------------------------------------------------
                PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete);
                // If the API provides a property for an external ICC profile, set it:
                // convertOptions.IccProfilePath = iccProfilePath;   // <-- uncomment if supported

                // Perform the conversion.  The method returns true on success.
                bool conversionResult = doc.Convert(convertOptions);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion to PDF/X‑4 failed.");
                    return;
                }

                // Save the converted document.  No special SaveOptions are required
                // because the document is already in the desired PDF/X‑4 format.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"MHT successfully converted to PDF/X‑4: {pdfOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}