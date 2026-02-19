using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - path to the source HTML file
        // args[1] - path for the generated PDF file
        // args[2] (optional) - path to a source PDF/A file to be converted to regular PDF
        // args[3] (optional) - path for the converted PDF/A output PDF

        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <htmlInput> <pdfOutput> [pdfaInput pdfaOutput]");
            return;
        }

        string htmlInput = args[0];
        string pdfOutput = args[1];

        // Convert HTML to PDF
        if (!File.Exists(htmlInput))
        {
            Console.Error.WriteLine($"HTML input file not found: {htmlInput}");
        }
        else
        {
            try
            {
                // Load the HTML document with default options
                HtmlLoadOptions loadOptions = new HtmlLoadOptions();
                Document htmlDoc = new Document(htmlInput, loadOptions);

                // Save as regular PDF
                htmlDoc.Save(pdfOutput);
                Console.WriteLine($"HTML successfully converted to PDF: {pdfOutput}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting HTML to PDF: {ex.Message}");
            }
        }

        // Optional: Convert PDF/A to regular PDF
        if (args.Length >= 4)
        {
            string pdfaInput = args[2];
            string pdfaOutput = args[3];

            if (!File.Exists(pdfaInput))
            {
                Console.Error.WriteLine($"PDF/A input file not found: {pdfaInput}");
            }
            else
            {
                try
                {
                    // Load the PDF/A document (no special options required)
                    Document pdfaDoc = new Document(pdfaInput);

                    // Save as regular PDF (same format, just a different file)
                    pdfaDoc.Save(pdfaOutput);
                    Console.WriteLine($"PDF/A successfully converted to regular PDF: {pdfaOutput}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error converting PDF/A to PDF: {ex.Message}");
                }
            }
        }
    }
}