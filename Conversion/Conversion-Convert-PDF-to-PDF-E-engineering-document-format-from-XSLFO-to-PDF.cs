using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.fo";
        const string outputPdfPath = "output.pdf";

        // Verify that the XSL‑FO source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Error: XSL‑FO file not found – {xslFoPath}");
            return;
        }

        try
        {
            // Load the XSL‑FO file into a PDF document using the correct load options type
            var loadOptions = new XslFoLoadOptions();
            var pdfDoc = new Document(xslFoPath, loadOptions);

            // Configure conversion to PDF/E (engineering) format
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            pdfDoc.Convert(conversionOptions);

            // Save the resulting PDF/E document
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF/E document successfully saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF/E conversion: {ex.Message}");
        }
    }
}