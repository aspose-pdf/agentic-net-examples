using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source EPUB and the target PDF/E files.
        const string epubPath   = "input.epub";
        const string pdfEPath   = "output_pdfe.pdf";
        const string logPath    = "conversion_log.txt";

        // Verify that the source file exists.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Source file not found: {epubPath}");
            return;
        }

        // Load the EPUB file using the dedicated load options.
        // EpubLoadOptions resides in the Aspose.Pdf namespace.
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Convert the document to the engineering PDF format.
            // Aspose.Pdf represents PDF/E via the PDF/X family; here we use PDF_X_3
            // as the closest supported format for engineering documents.
            // The Convert method writes any conversion errors to the specified log file.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document. The Save method without explicit SaveOptions
            // writes a PDF file (the format is determined by the prior Convert call).
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF/E: {pdfEPath}");
    }
}