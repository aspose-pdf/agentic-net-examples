using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source XML and the resulting PDF/E-1 file.
        const string xmlPath      = "input.xml";
        const string pdfE1Path    = "output_pdfe1.pdf";
        const string logPath      = "conversion_log.txt";

        // Verify the input file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Source XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML file into a PDF document using XmlLoadOptions.
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Convert the document to PDF/E-1 format.
                // The conversion writes any conversion warnings/errors to the log file.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted document as PDF/E-1.
                doc.Save(pdfE1Path);
            }

            Console.WriteLine($"XML successfully converted to PDF/E-1: {pdfE1Path}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}