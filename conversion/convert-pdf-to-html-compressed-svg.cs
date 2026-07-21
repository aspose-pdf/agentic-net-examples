using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
                // Enable compression of SVG graphics (produces SVGZ files)
                htmlOptions.CompressSvgGraphicsIfAny = true;

                // HTML conversion relies on GDI+; handle non‑Windows platforms gracefully
                try
                {
                    pdfDocument.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML saved to '{outputHtml}' with compressed SVG graphics.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}