using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <epubPath> <iccProfilePath> <outputPdfPath>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <epubPath> <iccProfilePath> <outputPdfPath>");
            return;
        }

        string epubPath = args[0];
        string iccPath = args[1];
        string outputPath = args[2];

        // Verify input files exist
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile file not found: {iccPath}");
            return;
        }

        try
        {
            // Load the EPUB file into a PDF document
            var loadOptions = new EpubLoadOptions();
            var pdfDocument = new Document(epubPath, loadOptions);

            // Prepare conversion options for PDF/X-4 with external ICC profile
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            conversionOptions.IccProfileFileName = iccPath;

            // Convert the document to PDF/X-4
            bool conversionResult = pdfDocument.Convert(conversionOptions);
            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion to PDF/X-4 failed.");
                return;
            }

            // Save the resulting PDF/X-4 file
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF/X-4 file saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
