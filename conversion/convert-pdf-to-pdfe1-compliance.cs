using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input2.pdf";
        const string outputPath = "output_pdfe1.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/E-1 compliance.
                // PdfFormat.PDF_E_1 specifies the PDF/E-1 standard.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted PDF/E-1 document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/E-1 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}