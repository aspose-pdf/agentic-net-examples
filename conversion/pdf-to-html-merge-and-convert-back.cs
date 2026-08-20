using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where intermediate HTML pages will be saved
        const string htmlOutputDir = "HtmlPages";

        // Base name for the generated HTML files (page.html, page_2.html, …)
        const string htmlBaseName = "page.html";

        // Path for the combined HTML file
        string combinedHtmlPath = Path.Combine(htmlOutputDir, "combined.html");

        // Final PDF file generated from the combined HTML
        const string finalPdfPath = "output.pdf";

        // Validate input
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(htmlOutputDir);

        try
        {
            // ------------------------------------------------------------
            // 1. Convert PDF to HTML – one HTML file per PDF page
            // ------------------------------------------------------------
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Configure HTML conversion options
                Aspose.Pdf.HtmlSaveOptions htmlSaveOpts = new Aspose.Pdf.HtmlSaveOptions
                {
                    // Generate a separate HTML file for each PDF page
                    SplitIntoPages = true,

                    // Embed raster images as PNG inside SVG (cross‑platform friendly)
                    RasterImagesSavingMode = Aspose.Pdf.HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Build the full path for the first HTML file; subsequent pages will be
                // created automatically with suffixes (_2, _3, …)
                string firstHtmlPath = Path.Combine(htmlOutputDir, htmlBaseName);

                // Perform the conversion
                pdfDoc.Save(firstHtmlPath, htmlSaveOpts);
            }

            // ------------------------------------------------------------
            // 2. Combine the generated HTML pages into a single file
            // ------------------------------------------------------------
            // Find all HTML files that match the naming pattern (page.html, page_2.html, …)
            var htmlFiles = Directory.GetFiles(htmlOutputDir, "page*.html")
                                     .OrderBy(f => f) // Ensure correct page order
                                     .ToList();

            if (htmlFiles.Count == 0)
            {
                Console.Error.WriteLine("No HTML files were generated.");
                return;
            }

            // Concatenate the contents of all HTML files
            StringBuilder combinedBuilder = new StringBuilder();
            foreach (string htmlFile in htmlFiles)
            {
                combinedBuilder.AppendLine(File.ReadAllText(htmlFile));
            }

            // Write the combined HTML to disk
            File.WriteAllText(combinedHtmlPath, combinedBuilder.ToString());

            // ------------------------------------------------------------
            // 3. Convert the combined HTML back to PDF
            // ------------------------------------------------------------
            Aspose.Pdf.HtmlLoadOptions htmlLoadOpts = new Aspose.Pdf.HtmlLoadOptions();

            using (Aspose.Pdf.Document htmlDoc = new Aspose.Pdf.Document(combinedHtmlPath, htmlLoadOpts))
            {
                // Save the resulting PDF
                htmlDoc.Save(finalPdfPath);
            }

            Console.WriteLine($"Conversion completed successfully.");
            Console.WriteLine($"Combined HTML saved to: {combinedHtmlPath}");
            Console.WriteLine($"Final PDF saved to: {finalPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}