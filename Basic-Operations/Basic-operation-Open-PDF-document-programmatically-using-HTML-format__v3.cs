using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Verify that the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize HtmlSaveOptions – required to produce HTML output
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Save the PDF as HTML. The HtmlSaveOptions ensures non‑PDF output.
            try
            {
                pdfDocument.Save(htmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only.
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }
}