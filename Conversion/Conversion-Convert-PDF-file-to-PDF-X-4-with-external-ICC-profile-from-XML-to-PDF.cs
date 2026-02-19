using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string xmlPath = Path.Combine(dataDir, "input.xml");          // source XML
        string iccPath = Path.Combine(dataDir, "profile.icc");        // external ICC profile
        string outputPath = Path.Combine(dataDir, "output.pdf");      // resulting PDF/X‑4

        // Verify required files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }
        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        try
        {
            // Load the XML document (PDFXML format) into an Aspose.Pdf Document
            PdfXmlLoadOptions loadOptions = new PdfXmlLoadOptions();
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Prepare conversion options for PDF/X‑4 and attach the ICC profile
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                conversionOptions.IccProfileFileName = iccPath;

                // Perform the format conversion
                pdfDocument.Convert(conversionOptions);

                // Save the converted document
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"Conversion successful. PDF/X‑4 saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}