using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputXml = "output.xml";         // final XML file
        const string logFile   = "conversion_log.txt"; // optional log for conversion errors

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
                // Convert the PDF to PDF/X‑3 format.
                // The Convert method returns a bool indicating success.
                // Errors are written to the specified log file.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the (now PDF/X‑3) document as XML.
                XmlSaveOptions xmlOpts = new XmlSaveOptions(); // no special options needed
                doc.Save(outputXml, xmlOpts);
            }

            Console.WriteLine($"Successfully converted '{inputPdf}' to PDF/X‑3 and saved as XML to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}