using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psPath = "input.ps";
        const string outputPdfPath = "output_pdfx4.pdf";
        const string iccProfilePath = "profile.icc";

        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"Source file not found: {psPath}");
            return;
        }

        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions
            using (Document doc = new Document())
            {
                PsLoadOptions loadOptions = new PsLoadOptions();
                doc.LoadFrom(psPath, loadOptions);

                // Configure conversion to PDF/X-4 with external ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete);
                convOptions.IccProfileFileName = iccProfilePath;

                // Perform the conversion
                doc.Convert(convOptions);

                // Save the resulting PDF/X-4 document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}