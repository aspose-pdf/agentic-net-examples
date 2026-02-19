using System;
using System.IO;
using Aspose.Pdf; // PdfFormatConversionOptions, PdfFormat, Document, CgmLoadOptions are in this namespace

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file and output PDF/E-1 file paths
        const string cgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file using default load options
            var loadOptions = new CgmLoadOptions();
            var pdfDocument = new Document(cgmPath, loadOptions);

            // Prepare conversion options to produce a PDF/E-1 document
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            // Additional conversion settings can be configured here if needed
            // e.g., conversionOptions.ErrorAction = ConvertErrorAction.Skip;

            // Perform the conversion to PDF/E-1
            pdfDocument.Convert(conversionOptions);

            // Save the resulting PDF/E-1 file
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF/E-1 file successfully created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}