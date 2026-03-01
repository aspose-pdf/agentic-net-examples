using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output XML file path (result of PDF → XML conversion)
        const string outputXmlPath = "output.xml";
        // Log file for conversion errors
        const string conversionLogPath = "conversion_log.xml";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Convert the document to PDF/X‑3 format.
                // Errors (if any) are written to the specified log file.
                pdfDoc.Convert(conversionLogPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the (now PDF/X‑3) document as XML.
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                pdfDoc.Save(outputXmlPath, xmlOptions);
            }

            Console.WriteLine($"Conversion completed. XML saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}