using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF/X file paths
        const string htmlPath      = "input.html";
        const string tempPdfPath   = "temp.pdf";
        const string pdfxPath      = "output_pdfx3.pdf";
        const string conversionLog = "conversion_log.txt";

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert HTML to a regular PDF (intermediate file)
        // -----------------------------------------------------------------
        try
        {
            // Load HTML using HtmlLoadOptions (requires GDI+ on Windows)
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save as PDF – no SaveOptions needed because PDF is the default format
                htmlDoc.Save(tempPdfPath);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation aborted.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML→PDF conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Convert the PDF to PDF/X‑3 format
        // -----------------------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(tempPdfPath))
            {
                // Convert to PDF/X‑3, writing any conversion warnings/errors to a log file
                pdfDoc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X‑3 document
                pdfDoc.Save(pdfxPath);
            }

            Console.WriteLine($"Successfully created PDF/X‑3 file: {pdfxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF→PDF/X conversion: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF if it exists
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}