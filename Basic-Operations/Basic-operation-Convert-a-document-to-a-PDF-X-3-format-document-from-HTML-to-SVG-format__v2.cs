using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath   = "input.html";          // Source HTML file
        const string svgPath    = "intermediate.svg";    // Temporary SVG file
        const string pdfX3Path = "output_pdfx3.pdf";    // Final PDF/X‑3 file
        const string logPath    = "conversion_log.xml"; // Log for conversion errors

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load HTML and save it as SVG
        // ------------------------------------------------------------
        try
        {
            // Load HTML using HtmlLoadOptions (Windows‑only GDI+ operation)
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save to SVG using SvgSaveOptions
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                htmlDoc.Save(svgPath, svgOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML → SVG conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipping on this platform.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML → SVG conversion: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Load SVG and convert to PDF/X‑3
        // ------------------------------------------------------------
        try
        {
            // Load the generated SVG using SvgLoadOptions
            using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
            {
                // Convert to PDF/X‑3, logging any conversion errors
                svgDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X‑3 document
                svgDoc.Save(pdfX3Path);
            }

            Console.WriteLine($"Successfully created PDF/X‑3: {pdfX3Path}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during SVG → PDF/X‑3 conversion: {ex.Message}");
        }
    }
}