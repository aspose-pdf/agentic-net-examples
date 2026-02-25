using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath = "intermediate.svg";
        const string pdfX3Path = "output_pdfx3.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // ---------- HTML → SVG ----------
        // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch.
        try
        {
            // Load HTML with explicit HtmlLoadOptions.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save as SVG using SvgSaveOptions (required to force SVG output).
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                htmlDoc.Save(svgPath, svgOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipping.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting HTML to SVG: {ex.Message}");
            return;
        }

        // ---------- SVG → PDF/X‑3 ----------
        try
        {
            // Load the generated SVG with explicit SvgLoadOptions.
            using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
            {
                // Convert to PDF/X‑3, logging any conversion errors.
                svgDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X‑3 document.
                svgDoc.Save(pdfX3Path);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting SVG to PDF/X-3: {ex.Message}");
            return;
        }

        Console.WriteLine($"Conversion completed. PDF/X-3 saved to '{pdfX3Path}'.");
    }
}