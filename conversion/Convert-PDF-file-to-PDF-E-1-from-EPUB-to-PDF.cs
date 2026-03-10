using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input EPUB, output PDF/E-1 and conversion log
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";
        const string logPath  = "conversion.log";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        try
        {
            // Load the EPUB file using default load options
            using (Document doc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Convert the document to PDF/E-1 format.
                // The conversion log will be written to logPath.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the resulting PDF/E-1 document.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"EPUB successfully converted to PDF/E-1: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}