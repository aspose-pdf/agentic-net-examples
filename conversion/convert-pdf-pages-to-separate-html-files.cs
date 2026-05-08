using System;
using System.IO;
using Aspose.Pdf;               // Core API and all SaveOptions are in this namespace

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

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true
                };

                // Save the document; Aspose.Pdf will generate multiple HTML files
                // (e.g., output.html, output_1.html, output_2.html, …)
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine("PDF pages have been converted to individual HTML files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}