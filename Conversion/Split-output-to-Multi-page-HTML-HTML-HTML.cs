using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, HtmlLoadOptions, HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputHtmlPath  = "input.html";               // source HTML file
        const string outputFolder   = "output_html_pages";        // folder for split pages

        // Verify source file exists
        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Load the HTML document (Aspose.Pdf can treat HTML as a source document)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document doc = new Document(inputHtmlPath, loadOptions))
        {
            // Configure HTML save options to generate one HTML file per PDF page
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,          // enable multi‑page output
                // Optional: create separate CSS per page
                // SplitCssIntoPages = false,
                // Optional: specify where images should be stored
                // SpecialFolderForAllImages = Path.Combine(outputFolder, "images")
            };

            // Base file name for the generated HTML pages
            string baseHtmlPath = Path.Combine(outputFolder, "output.html");

            // Save the document; Aspose.Pdf will create output.html, output_1.html, output_2.html, ...
            try
            {
                doc.Save(baseHtmlPath, saveOptions);
                Console.WriteLine($"Multi‑page HTML saved to folder: {outputFolder}");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}