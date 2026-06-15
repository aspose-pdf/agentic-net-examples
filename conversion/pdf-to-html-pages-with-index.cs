using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Folder where individual HTML pages and the index will be saved
        const string outputFolder = "HtmlPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // List to keep track of generated page file names (in order)
        List<string> pageFiles = new List<string>();

        // Configure HTML save options to split each PDF page into its own HTML file
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            SplitIntoPages = true,

            // Custom strategy to control how each page's HTML markup is saved
            CustomHtmlSavingStrategy = delegate (HtmlSaveOptions.HtmlPageMarkupSavingInfo htmlInfo)
            {
                // The suggested file name for this page (e.g., "output_1.html")
                string fileName = htmlInfo.SupposedFileName;

                // Full path for the page file
                string fullPath = Path.Combine(outputFolder, fileName);

                // Write the HTML content stream to the file
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    htmlInfo.ContentStream.CopyTo(fs);
                }

                // Record the file name for later index generation
                pageFiles.Add(fileName);
            }
        };

        // Load the PDF and perform the conversion
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The first argument is a dummy path; actual page files are saved via the custom strategy
            pdfDoc.Save(Path.Combine(outputFolder, "dummy.html"), htmlOptions);
        }

        // Generate an index.html that links to each individual page file
        string indexPath = Path.Combine(outputFolder, "index.html");
        using (StreamWriter writer = new StreamWriter(indexPath))
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

            foreach (string pageFile in pageFiles)
            {
                writer.WriteLine($"        <li><a href=\"{pageFile}\">{pageFile}</a></li>");
            }

            writer.WriteLine("    </ul>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Conversion complete. HTML pages and index saved to '{outputFolder}'.");
    }
}