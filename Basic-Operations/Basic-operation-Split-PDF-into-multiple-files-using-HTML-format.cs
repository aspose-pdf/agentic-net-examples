using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "HtmlPages";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the folder for the split HTML files exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to split each PDF page into its own HTML file
                HtmlSaveOptions options = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images directly into the generated HTML (optional but common)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Base file name for the generated pages; Aspose.Pdf will append page numbers
                string baseHtmlPath = Path.Combine(outputFolder, "page.html");

                try
                {
                    // Perform the conversion; each page becomes a separate HTML file
                    doc.Save(baseHtmlPath, options);
                    Console.WriteLine($"PDF successfully split into HTML pages in folder: {outputFolder}");
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
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}