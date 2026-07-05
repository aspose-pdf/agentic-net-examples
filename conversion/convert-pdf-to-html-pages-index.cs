using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string baseHtml = "output.html";        // base name for generated pages
        const string indexHtml = "index.html";        // index file to create

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // List to collect the names of the generated HTML page files
        List<string> generatedPages = new List<string>();

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options to split each PDF page into its own HTML file
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // Custom strategy to save each page's markup and capture the file name
            htmlOpts.CustomHtmlSavingStrategy = delegate (Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingInfo info)
            {
                // Determine the file name for this page
                string pageFileName = info.SupposedFileName;
                if (string.IsNullOrEmpty(pageFileName))
                {
                    // Fallback naming if the SDK does not provide one
                    pageFileName = $"page_{info.HtmlHostPageNumber}.html";
                }

                // Write the HTML content stream to the file
                using (FileStream fs = new FileStream(pageFileName, FileMode.Create, FileAccess.Write))
                {
                    info.ContentStream.CopyTo(fs);
                }

                // Record the generated file name for later index creation
                generatedPages.Add(pageFileName);
            };

            // Perform the conversion; the custom strategy handles actual file writing
            pdfDoc.Save(baseHtml, htmlOpts);
        }

        // Create an index.html that links to each generated page
        using (StreamWriter writer = new StreamWriter(indexHtml))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"UTF-8\">");
            writer.WriteLine("    <title>PDF Pages Index</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <h1>PDF Pages</h1>");
            writer.WriteLine("    <ul>");

            foreach (string pagePath in generatedPages)
            {
                string fileName = Path.GetFileName(pagePath);
                writer.WriteLine($"        <li><a href=\"{fileName}\">{fileName}</a></li>");
            }

            writer.WriteLine("    </ul>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Conversion complete. Index created at '{indexHtml}'.");
    }
}