using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for HTML pages exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document (lifecycle: using ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed all resources (images, CSS) into the generated HTML files
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Base file name for the generated HTML pages
                string baseHtmlPath = Path.Combine(outputFolder, "output.html");

                try
                {
                    // Save the PDF as split HTML pages
                    pdfDoc.Save(baseHtmlPath, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to split HTML pages in '{outputFolder}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}