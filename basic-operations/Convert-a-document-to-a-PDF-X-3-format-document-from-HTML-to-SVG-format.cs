using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string htmlPath   = "input.html";          // source HTML file
        const string pdfX3Path = "intermediate_pdfx3.pdf"; // PDF/X‑3 intermediate file
        const string svgPath   = "output.svg";          // final SVG file
        const string logPath   = "conversion_log.txt"; // optional log for conversion errors

        // Verify source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Load HTML and convert it to PDF/X‑3
            // -----------------------------------------------------------------
            // HtmlLoadOptions is required for HTML input.
            // Document implements IDisposable, so wrap it in a using block.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Convert the document to PDF/X‑3 format.
                // The Convert method writes conversion errors to the supplied log file.
                bool conversionResult = htmlDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion to PDF/X‑3 reported errors. Check the log file.");
                }

                // Save the PDF/X‑3 document to disk.
                htmlDoc.Save(pdfX3Path);
            }

            // -----------------------------------------------------------------
            // 2. Load the PDF/X‑3 file and save it as SVG
            // -----------------------------------------------------------------
            using (Document pdfX3Doc = new Document(pdfX3Path))
            {
                // Initialize SVG save options (default constructor is sufficient for most cases).
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG. The output may consist of multiple SVG files
                // (one per page) if the source PDF has more than one page.
                pdfX3Doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML → PDF/X‑3 → SVG conversion completed successfully.");
            Console.WriteLine($"PDF/X‑3 file: {pdfX3Path}");
            Console.WriteLine($"SVG file(s): {svgPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}