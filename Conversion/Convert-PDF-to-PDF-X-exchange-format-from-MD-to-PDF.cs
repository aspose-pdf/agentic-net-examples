using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input Markdown, intermediate PDF (optional), and final PDF/X output
        const string mdPath       = "input.md";
        const string pdfXPath     = "output_pdfx3.pdf";
        const string conversionLog = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"Input file not found: {mdPath}");
            return;
        }

        try
        {
            // Load the Markdown file using MdLoadOptions
            MdLoadOptions mdLoadOptions = new MdLoadOptions();
            using (Document doc = new Document(mdPath, mdLoadOptions))
            {
                // Convert the document to PDF/X-3 format.
                // The Convert method logs conversion issues to the specified file.
                doc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as PDF/X-3.
                doc.Save(pdfXPath);
            }

            Console.WriteLine($"Successfully converted '{mdPath}' to PDF/X-3 at '{pdfXPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}