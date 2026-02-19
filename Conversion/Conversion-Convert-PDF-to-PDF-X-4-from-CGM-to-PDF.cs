using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file path (first argument or default)
        string cgmPath = args.Length > 0 ? args[0] : "input.cgm";
        // Output PDF file path (second argument or default)
        string pdfPath = args.Length > 1 ? args[1] : "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file using default load options
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document pdfDocument = new Document(cgmPath, loadOptions))
            {
                // Set conversion options to PDF/X-4 format
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);

                // Perform the format conversion
                pdfDocument.Convert(conversionOptions);

                // Save the resulting PDF/X-4 document
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF/X-4 saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
