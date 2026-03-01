using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // for HtmlLoadOptions if needed (actually in Aspose.Pdf namespace)
 
class Program
{
    static void Main()
    {
        const string htmlInputPath  = "input.html";   // source HTML file
        const string svgOutputPath  = "output.svg";   // desired SVG file
        const string conversionLog  = "convert.log"; // log for PDF/A or PDF/X conversion

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML document. This operation requires GDI+ and therefore may fail on non‑Windows platforms.
            using (Document doc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Convert the document to PDF/A (you can change to PDF/X by using PdfFormat.PDF_X_3).
                // The Convert method changes the internal PDF version and writes any conversion warnings to the log file.
                bool converted = doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/A failed. Check the log for details.");
                    return;
                }

                // Save the resulting PDF/A document as SVG using explicit SvgSaveOptions.
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgOutputPath, svgOptions);
            }

            Console.WriteLine($"HTML → PDF/A → SVG conversion completed successfully.\nSVG saved to: {svgOutputPath}");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available (e.g., on macOS/Linux). Inform the user that HTML conversion cannot be performed.
            Console.WriteLine("HTML loading requires Windows GDI+. Skipping conversion on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}