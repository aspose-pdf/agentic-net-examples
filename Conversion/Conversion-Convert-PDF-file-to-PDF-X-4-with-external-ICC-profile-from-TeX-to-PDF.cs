using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input TeX file, output PDF file and external ICC profile
        const string texPath = "input.tex";
        const string outputPdfPath = "output.pdf";
        const string iccProfilePath = "profile.icc";

        // Verify that required files exist
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX source file not found: {texPath}");
            return;
        }

        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile file not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load TeX file into a PDF document using TeXLoadOptions
            var texLoadOptions = new TeXLoadOptions();
            var pdfDocument = new Document(texPath, texLoadOptions);

            // Prepare conversion options: target PDF/X-4 format with external ICC profile
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            conversionOptions.IccProfileFileName = iccProfilePath;

            // Convert the document to PDF/X-4 using the specified ICC profile
            pdfDocument.Convert(conversionOptions);

            // Save the resulting PDF/X-4 document
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF/X-4 file saved successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}