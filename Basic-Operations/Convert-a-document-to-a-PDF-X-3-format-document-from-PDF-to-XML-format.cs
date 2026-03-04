using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlOutput = "output.xml";
        const string logFile = "conversion_log.xml";

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
                // Convert the document to PDF/X-3 format.
                // Errors are written to the specified log file.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the (now PDF/X-3) document as XML.
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                doc.Save(xmlOutput, xmlOptions);
            }

            Console.WriteLine($"PDF/X-3 conversion completed and saved as XML: {xmlOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}