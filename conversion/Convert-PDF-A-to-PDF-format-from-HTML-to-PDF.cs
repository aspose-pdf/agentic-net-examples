using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Convert an HTML file to a PDF document.
        // -----------------------------------------------------------------
        const string htmlInputPath   = "input.html";          // source HTML
        const string pdfFromHtmlPath = "output_from_html.pdf"; // destination PDF

        if (File.Exists(htmlInputPath))
        {
            // Load the HTML file with default options.
            HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions();

            // Document constructor loads the file; using ensures proper disposal.
            using (Document htmlDoc = new Document(htmlInputPath, htmlLoadOptions))
            {
                // Save as PDF – no SaveOptions needed because the target format is PDF.
                htmlDoc.Save(pdfFromHtmlPath);
            }
        }
        else
        {
            Console.Error.WriteLine($"HTML source not found: {htmlInputPath}");
        }

        // -----------------------------------------------------------------
        // 2. Convert a PDF/A document to a regular PDF.
        // -----------------------------------------------------------------
        const string pdfaInputPath   = "input_pdfa.pdf";          // source PDF/A
        const string pdfFromPdfAPath = "output_from_pdfa.pdf";    // destination PDF

        if (File.Exists(pdfaInputPath))
        {
            // Load the PDF/A file. The Document class automatically detects the format.
            using (Document pdfaDoc = new Document(pdfaInputPath))
            {
                // Remove PDF/A compliance flags – the document becomes a normal PDF.
                pdfaDoc.RemovePdfaCompliance();

                // Save the result as a standard PDF.
                pdfaDoc.Save(pdfFromPdfAPath);
            }
        }
        else
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
        }

        Console.WriteLine("Conversion completed.");
    }
}