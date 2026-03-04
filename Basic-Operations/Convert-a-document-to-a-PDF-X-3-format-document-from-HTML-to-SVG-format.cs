using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file
        const string htmlPath = "input.html";

        // Intermediate PDF (HTML → PDF)
        const string pdfPath = "intermediate.pdf";

        // PDF/X‑3 output (PDF → PDF/X‑3)
        const string pdfX3Path = "output_pdfx3.pdf";

        // Final SVG output (PDF/X‑3 → SVG)
        const string svgPath = "output.svg";

        // Log file for conversion errors
        const string logPath = "conversion.log";

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert HTML to PDF
        // -----------------------------------------------------------------
        try
        {
            // HtmlLoadOptions is required for loading HTML documents.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save as a regular PDF (intermediate file)
                htmlDoc.Save(pdfPath);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ and works only on Windows.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Skipping this step.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting HTML to PDF: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Convert PDF to PDF/X‑3
        // -----------------------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Convert to PDF/X‑3, logging any conversion issues.
                pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the PDF/X‑3 version (optional, can be omitted if only SVG is needed)
                pdfDoc.Save(pdfX3Path);

                // -----------------------------------------------------------------
                // Step 3: Save the PDF/X‑3 document as SVG
                // -----------------------------------------------------------------
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pdfDoc.Save(svgPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF/X‑3 conversion or SVG export: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the intermediate PDF if it is no longer needed.
            try { if (File.Exists(pdfPath)) File.Delete(pdfPath); } catch { }
        }

        Console.WriteLine($"Conversion completed successfully:");
        Console.WriteLine($"  PDF/X‑3 file: {pdfX3Path}");
        Console.WriteLine($"  SVG file   : {svgPath}");
    }
}