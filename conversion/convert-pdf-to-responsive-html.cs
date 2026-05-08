using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToResponsiveHtml
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string htmlPath     = "output.html";
        const string cssPath      = "responsive.css";   // Existing mobile‑friendly stylesheet
        const string tempHtmlPath = "temp_output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS not found: {cssPath}");
            return;
        }

        try
        {
            // ---------- Convert PDF → HTML ----------
            using (Document pdfDoc = new Document(pdfPath))
            {
                // HtmlSaveOptions must be supplied – otherwise Save() writes PDF.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Use embedded PNG images wrapped in SVG (cross‑platform friendly)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Generate a single HTML file (easier to inject CSS)
                    SplitIntoPages = false,
                    // Optional: make layout flow so pages adapt to screen width
                    FixedLayout = false,
                    // Optional: enable responsive flow of pages based on viewer size
                    PagesFlowTypeDependsOnViewersScreenSize = true
                };

                // Save to a temporary HTML file first
                pdfDoc.Save(tempHtmlPath, htmlOpts);
            }

            // ---------- Inject responsive CSS ----------
            // Read the generated HTML
            string htmlContent = File.ReadAllText(tempHtmlPath);

            // Prepare the <link> tag for the stylesheet
            string cssLinkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssPath)}\" />";

            // Insert the <link> just before the closing </head> tag.
            // If </head> is not found, prepend the link at the start of the file.
            int headCloseIdx = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headCloseIdx >= 0)
            {
                htmlContent = htmlContent.Insert(headCloseIdx, cssLinkTag + Environment.NewLine);
            }
            else
            {
                htmlContent = cssLinkTag + Environment.NewLine + htmlContent;
            }

            // Write the final HTML with the CSS reference
            File.WriteAllText(htmlPath, htmlContent);

            // Clean up the temporary file
            File.Delete(tempHtmlPath);

            Console.WriteLine($"PDF successfully converted to responsive HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}