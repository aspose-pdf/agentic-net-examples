using System;
using System.IO;
using Aspose.Pdf;

class PdfToResponsiveHtml
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string responsiveCssPath = "responsive.css";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(responsiveCssPath))
        {
            Console.Error.WriteLine($"CSS file not found: {responsiveCssPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed images as PNG inside SVG to keep a single HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Fixed layout is false to allow flow layout (better for responsive design)
                    FixedLayout = false,
                    // Optional: set a title for the generated HTML page
                    Title = Path.GetFileNameWithoutExtension(pdfPath)
                };

                // Convert PDF to HTML
                pdfDoc.Save(htmlPath, htmlOptions);
            }

            // At this point the HTML file exists. Insert a link to the responsive CSS.
            string htmlContent = File.ReadAllText(htmlPath);

            // Build the <link> tag for the stylesheet
            string cssLinkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(responsiveCssPath)}\" />";

            // Insert the <link> tag just before the closing </head> tag.
            // If </head> is not found, prepend the link at the start of the file.
            int headCloseIndex = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headCloseIndex >= 0)
            {
                htmlContent = htmlContent.Insert(headCloseIndex, cssLinkTag + Environment.NewLine);
            }
            else
            {
                // Fallback: prepend to the beginning
                htmlContent = cssLinkTag + Environment.NewLine + htmlContent;
            }

            // Write the modified HTML back to the same file
            File.WriteAllText(htmlPath, htmlContent);
            Console.WriteLine($"PDF successfully converted to responsive HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ which is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}