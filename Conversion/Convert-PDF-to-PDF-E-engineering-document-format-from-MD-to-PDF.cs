using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source Markdown file, the conversion log, and the resulting PDF/E file.
        const string mdPath      = "input.md";
        const string logPath     = "conversion.log";
        const string pdfEPath    = "output_pdfe.pdf";

        // Verify that the source file exists.
        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"Source file not found: {mdPath}");
            return;
        }

        // Load the Markdown file using MdLoadOptions.
        MdLoadOptions loadOptions = new MdLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(mdPath, loadOptions))
        {
            // Convert the document to PDF/E (engineering PDF) format.
            // The log file will contain any conversion warnings or errors.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document. After conversion the document is already in PDF/E format,
            // so a simple Save without explicit SaveOptions is sufficient.
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E saved to '{pdfEPath}'.");
    }
}