using System;
using System.IO;
using Aspose.Pdf; // Document, XslFoLoadOptions, PdfFormatConversionOptions, PdfFormat, SaveFormat

class XslFoToPdfXConverter
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string xslFoPath = Path.Combine(dataDir, "sample.fo");          // XSL‑FO source file
        string outputPdfXPath = Path.Combine(dataDir, "sample_pdfx.pdf"); // Resulting PDF/X file

        // Verify input file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Input XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Load the XSL‑FO file into a Document using XslFoLoadOptions
            XslFoLoadOptions loadOptions = new XslFoLoadOptions(); // no external XSL needed
            using (Document pdfDoc = new Document(xslFoPath, loadOptions))
            {
                // Prepare conversion options to produce PDF/X‑1A (you can choose another PDF/X format)
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_1A);

                // Convert the document to the desired PDF/X format
                pdfDoc.Convert(conversionOptions);

                // Save the converted document as a regular PDF file (the content is now PDF/X)
                pdfDoc.Save(outputPdfXPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion successful. PDF/X saved to: {outputPdfXPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
