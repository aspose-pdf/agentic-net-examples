using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string logFile    = "conversion.log";    // conversion log
        const string outputXml  = "output.xml";        // final XML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Convert the document to PDF/X-3 format.
                // Errors are written to the log file.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the (now PDF/X-3 compliant) document as XML.
                XmlSaveOptions xmlOpts = new XmlSaveOptions();
                doc.Save(outputXml, xmlOpts);
            }

            Console.WriteLine($"PDF → PDF/X-3 → XML conversion completed. Output: {outputXml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}