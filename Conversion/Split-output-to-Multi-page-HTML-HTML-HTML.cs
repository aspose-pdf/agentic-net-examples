using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtmlPath = "input.html";
        const string outputFolder   = "output_html_pages";

        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        // Ensure the folder for the split pages exists.
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source HTML document. HtmlLoadOptions is in Aspose.Pdf namespace.
            using (Document doc = new Document(inputHtmlPath, new HtmlLoadOptions()))
            {
                // Configure HTML save options to split each PDF page (here each HTML page) into its own HTML file.
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,          // Enable multi‑page output.
                    SplitCssIntoPages = false,      // One common CSS file (optional).
                    // Optional: specify where images and other resources should be placed.
                    SpecialFolderForAllImages = outputFolder
                };

                // When SplitIntoPages is true, Aspose.Pdf creates files:
                // output.html (first page), output_1.html, output_2.html, ...
                string baseOutputPath = Path.Combine(outputFolder, "output.html");
                doc.Save(baseOutputPath, saveOptions);
            }

            Console.WriteLine("HTML successfully split into multiple pages.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}