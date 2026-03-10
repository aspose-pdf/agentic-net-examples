using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, PdfFormat, ConvertErrorAction, etc.

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlInputPath   = "input.xml";
        const string pdfOutputPath  = "output.pdfe.pdf";   // PDF/E (engineered) output
        const string logFilePath    = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML file and create an initial PDF document.
            // XmlLoadOptions tells Aspose.Pdf to treat the source as XML.
            using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Convert the document to PDF/E (engineered) format.
                // PDF/E is based on PDF/X‑3, so we use PdfFormat.PDF_X_3.
                // ConvertErrorAction.Delete tells the engine to drop objects it cannot convert.
                pdfDoc.Convert(logFilePath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/E document.
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"XML successfully converted to PDF/E: {pdfOutputPath}");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing.
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}