using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF that contains a ZUGFeRD XML attachment
        const string inputPdfPath = "input.pdf";
        // Output PDF/A‑3U file – the XML attachment will be preserved
        const string outputPdfPath = "output_pdfa3u.pdf";
        // Optional log file for conversion details
        const string logPath = "conversion_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Convert the document to PDF/A‑3U. Embedded files (e.g., ZUGFeRD XML) are kept by default.
                doc.Convert(logPath, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑3U document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑3U file created successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
