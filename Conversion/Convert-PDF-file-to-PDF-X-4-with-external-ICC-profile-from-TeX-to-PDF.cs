using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_pdfx4.pdf";
        const string iccProfilePath = "myprofile.icc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Configure conversion to PDF/X-4 with external ICC profile
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                options.IccProfileFileName = iccProfilePath;
                options.ErrorAction = ConvertErrorAction.Delete; // handle conversion errors

                // Perform the conversion
                bool conversionResult = doc.Convert(options);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion completed with errors.");
                }

                // Save the converted PDF/X-4 document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/X-4 file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}