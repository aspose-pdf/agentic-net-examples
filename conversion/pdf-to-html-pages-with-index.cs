using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";                 // source PDF
        const string outputHtmlDir = "HtmlPages";                // folder for per‑page HTML files
        const string indexHtmlPath = "index.html";               // index file to create

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputHtmlDir);

        // List to keep the generated page file names (relative to the index file)
        List<string> pageFiles = new List<string>();

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure HTML save options to split into separate pages
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // Custom strategy – called for each generated HTML page
            htmlOptions.CustomHtmlSavingStrategy = delegate (Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingInfo htmlInfo)
            {
                // Determine a file name for this page
                string fileName = htmlInfo.SupposedFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    // Fallback: use the HTML host page number
                    fileName = $"page_{htmlInfo.HtmlHostPageNumber}.html";
                }

                string fullPath = Path.Combine(outputHtmlDir, fileName);

                // Write the HTML content stream to the file
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    htmlInfo.ContentStream.CopyTo(fs);
                }

                // Store the relative path for the index page
                pageFiles.Add(fileName);
            };

            // The actual path passed to Save is irrelevant when using a custom strategy,
            // but a valid string is required.
            string dummyPath = Path.Combine(outputHtmlDir, "dummy.html");
            pdfDocument.Save(dummyPath, htmlOptions);
        }

        // Build the index.html that links to each page file
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("    <meta charset=\"UTF-8\">");
        sb.AppendLine("    <title>PDF Pages Index</title>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("    <h1>PDF Pages</h1>");
        sb.AppendLine("    <ul>");

        foreach (string pageFile in pageFiles)
        {
            // Use a relative link from the index file to the page file
            sb.AppendLine($"        <li><a href=\"{outputHtmlDir}/{pageFile}\">{pageFile}</a></li>");
        }

        sb.AppendLine("    </ul>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        // Write the index file
        File.WriteAllText(indexHtmlPath, sb.ToString());

        Console.WriteLine($"Conversion complete. Pages saved in '{outputHtmlDir}'. Index created at '{indexHtmlPath}'.");
    }
}