using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputHtml = "output.html";       // base name for HTML files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion to generate one HTML file per PDF page
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // HTML conversion uses GDI+ and works only on Windows.
            // Wrap the save call to avoid runtime errors on non‑Windows platforms.
            try
            {
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine("PDF split into separate HTML pages successfully.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            }
        }
    }
}