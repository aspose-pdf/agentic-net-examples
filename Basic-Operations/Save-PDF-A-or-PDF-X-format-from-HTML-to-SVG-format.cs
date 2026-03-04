using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string htmlInputPath = "input.html";
        const string pdfaTempPath   = "temp_pdfa.pdf";   // optional intermediate PDF/A file
        const string svgOutputPath  = "output.svg";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is required for HTML input.
            using (Document doc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Convert the loaded document to PDF/A (PDF/A‑1B in this example).
                // The conversion log is optional; it records any conversion issues.
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Optional: save the intermediate PDF/A file if you need it.
                doc.Save(pdfaTempPath);

                // Save the (now PDF/A) document as SVG.
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgOutputPath, svgOptions);
            }

            Console.WriteLine($"SVG file created at '{svgOutputPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+, which is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}