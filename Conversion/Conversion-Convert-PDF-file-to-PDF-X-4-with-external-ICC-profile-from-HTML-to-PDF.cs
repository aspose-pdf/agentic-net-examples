using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string iccProfilePath = "profile.icc";
        const string outputPdfPath = "output_pdfx4.pdf";

        // Verify input files exist
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the HTML file into a PDF document
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Set up conversion options for PDF/X-4 with an external ICC profile
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete);
            convOptions.IccProfileFileName = iccProfilePath;

            // Perform the conversion to PDF/X-4
            bool conversionResult = doc.Convert(convOptions);
            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion to PDF/X-4 failed.");
                return;
            }

            // Save the resulting PDF/X-4 document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPdfPath}'.");
    }
}