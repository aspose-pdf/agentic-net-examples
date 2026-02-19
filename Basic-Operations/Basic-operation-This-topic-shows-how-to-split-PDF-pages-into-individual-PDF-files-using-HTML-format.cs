using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions and HtmlLoadOptions are classes in this namespace

class Program
{
    static void Main()
    {
        try
        {
            // Input PDF file
            const string pdfPath = "input.pdf";

            // Folder where intermediate HTML pages and final PDFs will be stored
            const string outputFolder = "SplitOutput";

            // Verify that the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the source PDF document
            Document pdfDocument = new Document(pdfPath);

            // --------------------------------------------------------------------
            // Step 1 – Convert each PDF page to a separate HTML file
            // --------------------------------------------------------------------
            var htmlOptions = new HtmlSaveOptions
            {
                // One HTML file per PDF page
                SplitIntoPages = true,

                // Only the content inside <body> is written – useful for further processing
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

                // Optional: give each HTML file a meaningful title prefix
                Title = "Page"
            };

            // The file name supplied here is used as a base name; Aspose will append
            // the page number (e.g., Page_1.html, Page_2.html, …) because SplitIntoPages is true.
            string htmlBasePath = Path.Combine(outputFolder, "Page.html");
            pdfDocument.Save(htmlBasePath, htmlOptions);

            Console.WriteLine($"PDF successfully split into {pdfDocument.Pages.Count} HTML pages.");

            // --------------------------------------------------------------------
            // Step 2 – Convert each generated HTML page back to an individual PDF
            // --------------------------------------------------------------------
            // Retrieve the list of HTML files that were created in the previous step.
            string[] htmlFiles = Directory.GetFiles(outputFolder, "Page_*.html");

            int pageIndex = 1;
            foreach (string htmlFile in htmlFiles)
            {
                // Load the HTML page. HtmlLoadOptions tells Aspose.Pdf to treat the source as HTML.
                var htmlDocument = new Document(htmlFile, new HtmlLoadOptions());

                // Save the HTML page as a separate PDF file.
                string pdfPagePath = Path.Combine(outputFolder, $"Page_{pageIndex}.pdf");
                htmlDocument.Save(pdfPagePath);

                Console.WriteLine($"Created PDF for page {pageIndex}: {pdfPagePath}");
                pageIndex++;
            }

            Console.WriteLine("All pages have been split into individual PDF files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}