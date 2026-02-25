using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input SVG file
        const string svgPath = "input.svg";

        // Optional intermediate PDF (can be omitted if not needed)
        const string intermediatePdfPath = "intermediate.pdf";

        // Final PDF/X-3 output file
        const string pdfxPath = "output_pdfx3.pdf";

        // Log file for conversion errors
        const string logPath = "conversion_log.xml";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        try
        {
            // Load SVG and convert it to a PDF document in memory
            using (Document doc = new Document(svgPath, new SvgLoadOptions()))
            {
                // Save the intermediate PDF (optional, useful for inspection)
                doc.Save(intermediatePdfPath);

                // Convert the PDF document to PDF/X-3 format.
                // ConvertErrorAction.Delete – objects that cannot be converted are removed.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X-3 document
                doc.Save(pdfxPath);
            }

            Console.WriteLine($"SVG successfully converted to PDF/X-3: {pdfxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}