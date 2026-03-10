using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";          // Source SVG file
        const string pdfPath = "intermediate.pdf";   // Temporary PDF file
        const string pdfxPath = "output_pdfx3.pdf";  // Final PDF/X‑3 file
        const string logPath = "conversion_log.txt"; // Log for conversion errors

        // Verify the SVG source exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // ---------- Load SVG and save as PDF ----------
        // Configure SVG loading options (choose the newer conversion engine)
        SvgLoadOptions loadOptions = new SvgLoadOptions();
        loadOptions.ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine;

        // Load the SVG and immediately save it as a PDF document
        using (Document doc = new Document(svgPath, loadOptions))
        {
            doc.Save(pdfPath); // Saves as PDF because the target extension is .pdf
        }

        // ---------- Convert the PDF to PDF/X‑3 ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Convert to PDF/X‑3, logging any conversion errors
            pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document
            pdfDoc.Save(pdfxPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF/X‑3: {pdfxPath}");
    }
}