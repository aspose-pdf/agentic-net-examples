using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";          // Source HTML file
        const string tempPdfPath = "temp_pdfa.pdf";    // Intermediate PDF/A file
        const string svgPath = "output.svg";           // Final SVG output
        const string logPath = "conversion_log.xml";   // Conversion log

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document (requires GDI+ on Windows)
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Convert HTML to PDF/A (PDF/A-1b used as example) and log any errors
                htmlDoc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the intermediate PDF/A file
                htmlDoc.Save(tempPdfPath);
            }

            // Load the intermediate PDF/A and save it as SVG
            using (Document pdfDoc = new Document(tempPdfPath))
            {
                SvgSaveOptions svgOptions = new SvgSaveOptions(); // Explicit SaveOptions for SVG
                pdfDoc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"SVG file saved to '{svgPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF/A file
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { }
            }
        }
    }
}