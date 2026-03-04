using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string intermediatePdfPath = "intermediate.pdf";
        const string pdfx3Path = "output_pdfx3.pdf";
        const string svgPath = "output.svg";
        const string conversionLog = "convert_log.xml";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load HTML and save as a temporary PDF.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                htmlDoc.Save(intermediatePdfPath);
            }

            // Load the temporary PDF and convert it to PDF/X-3 format.
            using (Document pdfDoc = new Document(intermediatePdfPath))
            {
                // Convert to PDF/X-3, logging any conversion errors.
                pdfDoc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                // Save the PDF/X-3 document.
                pdfDoc.Save(pdfx3Path);
            }

            // Load the PDF/X-3 document and export it to SVG.
            using (Document pdfForSvg = new Document(pdfx3Path))
            {
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pdfForSvg.Save(svgPath, svgOptions);
            }

            Console.WriteLine("Conversion completed:");
            Console.WriteLine($"HTML → PDF: {intermediatePdfPath}");
            Console.WriteLine($"PDF → PDF/X-3: {pdfx3Path}");
            Console.WriteLine($"PDF/X-3 → SVG: {svgPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}