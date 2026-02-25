using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath   = "input.epub";
        const string pdfEPath   = "output.pdfe.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        try
        {
            // Load the EPUB file and convert it to a PDF document in memory
            using (Document doc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Convert the document to PDF/E (engineering PDF) format.
                // The conversion log is written to logPath.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the resulting PDF/E document.
                doc.Save(pdfEPath);
            }

            Console.WriteLine($"EPUB successfully converted to PDF/E: {pdfEPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}