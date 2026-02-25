using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath   = "input.html";
        const string svgPath    = "intermediate.svg";
        const string pdfX3Path  = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Verify input file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load HTML and save as SVG
        // -----------------------------------------------------------------
        try
        {
            // HtmlLoadOptions is required for HTML input (GDI+ may be needed on Windows)
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save to SVG using default options (all SaveOptions live in Aspose.Pdf namespace)
                htmlDoc.Save(svgPath, new SvgSaveOptions());
                Console.WriteLine($"HTML → SVG saved to '{svgPath}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML → SVG conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipping.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML→SVG conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Load the generated SVG and convert to PDF/X‑3
        // -----------------------------------------------------------------
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        try
        {
            // SvgLoadOptions can specify the conversion engine if desired
            var svgLoadOptions = new SvgLoadOptions
            {
                // Example: use the newer conversion engine
                // ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
            };

            using (Document svgDoc = new Document(svgPath, svgLoadOptions))
            {
                // Convert to PDF/X‑3 format; log any conversion messages
                svgDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the final PDF/X‑3 document
                svgDoc.Save(pdfX3Path);
                Console.WriteLine($"SVG → PDF/X‑3 saved to '{pdfX3Path}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during SVG→PDF/X‑3 conversion: {ex.Message}");
        }
    }
}