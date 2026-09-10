using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where individual HTML pages will be written
        const string outputDir = "HtmlPages";

        // Base file name for the generated HTML files.
        // When SplitIntoPages is true, Aspose.Pdf will create files like:
        // page.html, page_page1.html, page_page2.html, ...
        const string baseHtmlFile = "page.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
                // Additional options can be set here, e.g.:
                // SpecialFolderForAllImages = outputDir
            };

            // Full path to the base HTML file (the converter will create additional files as needed)
            string outputPath = Path.Combine(outputDir, baseHtmlFile);

            // Save the document as HTML with the configured options
            pdfDoc.Save(outputPath, htmlOptions);
        }

        Console.WriteLine("PDF pages have been converted to individual HTML files.");
    }
}