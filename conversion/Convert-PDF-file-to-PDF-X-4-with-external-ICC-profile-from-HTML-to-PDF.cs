using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file, external ICC profile and output PDF/X‑4 file paths
        const string htmlPath = "input.html";
        const string iccProfilePath = "profile.icc";
        const string outputPdfX4Path = "output_pdfx4.pdf";

        // Verify required files exist
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions (requires GDI+ on Windows)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Prepare conversion options for PDF/X‑4 with the external ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convOptions.IccProfileFileName = iccProfilePath;

                // Convert the in‑memory PDF to PDF/X‑4
                doc.Convert(convOptions);

                // Save the resulting PDF/X‑4 document
                doc.Save(outputPdfX4Path);
            }

            Console.WriteLine($"PDF/X‑4 file saved to '{outputPdfX4Path}'.");
        }
        // HTML‑to‑PDF conversion uses GDI+, which is Windows‑only
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("HTML to PDF conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}