using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "html_output";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // List to keep generated page file names
        List<string> pageFiles = new List<string>();

        // Configure HTML save options to split each PDF page into its own HTML file
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            SplitIntoPages = true
        };

        // Custom strategy to save each generated HTML page manually
        htmlOptions.CustomHtmlSavingStrategy = (Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingInfo info) =>
        {
            // Build a file name like "page_1.html", "page_2.html", etc.
            string pageFileName = $"page_{info.HtmlHostPageNumber}.html";
            string pageFilePath = Path.Combine(outputFolder, pageFileName);

            // Write the HTML content stream to the file
            using (FileStream fs = new FileStream(pageFilePath, FileMode.Create, FileAccess.Write))
            {
                info.ContentStream.CopyTo(fs);
            }

            // Record the file name for index generation
            pageFiles.Add(pageFileName);
        };

        // Perform the conversion
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // The target path is required but actual files are created by the custom strategy above
            string dummyTarget = Path.Combine(outputFolder, "dummy.html");
            pdfDocument.Save(dummyTarget, htmlOptions);
        }

        // Generate index.html linking to each page file
        string indexPath = Path.Combine(outputFolder, "index.html");
        using (StreamWriter writer = new StreamWriter(indexPath))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"UTF-8\">");
            writer.WriteLine("    <title>PDF Pages Index</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <h1>PDF Pages</h1>");
            writer.WriteLine("    <ul>");

            foreach (string pageFile in pageFiles)
            {
                writer.WriteLine($"        <li><a href=\"{pageFile}\">{pageFile}</a></li>");
            }

            writer.WriteLine("    </ul>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Conversion complete. HTML files and index.html are located in '{outputFolder}'.");
    }
}