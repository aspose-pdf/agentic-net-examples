using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where individual HTML pages will be saved
        const string outputFolder = "HtmlPages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // Base file name for the first page; subsequent pages will be named
            // page.html, page_1.html, page_2.html, etc., in the same folder
            string baseHtmlPath = Path.Combine(outputFolder, "page.html");

            // Save the document using the configured options
            pdfDoc.Save(baseHtmlPath, htmlOptions);
        }

        Console.WriteLine("PDF has been converted to individual HTML pages.");
    }
}