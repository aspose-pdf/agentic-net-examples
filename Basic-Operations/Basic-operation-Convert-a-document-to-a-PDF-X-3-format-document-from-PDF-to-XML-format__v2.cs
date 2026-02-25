using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output XML file path
        const string outputXml = "output.xml";

        // Path for conversion log (optional but recommended)
        const string logFile = "conversion_log.xml";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Convert the PDF to PDF/X-3 format.
                // The Convert method writes a log file and applies the specified PDF format.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Prepare XML save options (required for non‑PDF output)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the converted document as XML
                doc.Save(outputXml, xmlOptions);
            }

            Console.WriteLine($"PDF → PDF/X-3 → XML conversion completed. Output: {outputXml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}