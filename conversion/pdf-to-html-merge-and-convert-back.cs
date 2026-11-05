using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string htmlOutputFolder = "html_pages";
        const string combinedHtmlPath = "combined.html";
        const string finalPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for split HTML pages exists
        Directory.CreateDirectory(htmlOutputFolder);

        // -------------------------------------------------
        // Step 1: Convert PDF to HTML (one file per page)
        // -------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                var htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save the PDF as HTML pages into the specified folder
                pdfDoc.Save(htmlOutputFolder, htmlOpts);

                // Rename generated files to a predictable pattern (page_1.html, page_2.html, ...)
                var generatedFiles = Directory.GetFiles(htmlOutputFolder, "*.html");
                Array.Sort(generatedFiles);
                for (int i = 0; i < generatedFiles.Length; i++)
                {
                    string newFileName = Path.Combine(htmlOutputFolder, $"page_{i + 1}.html");
                    File.Move(generatedFiles[i], newFileName, true);
                }
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML generation.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Step 2: Combine the split HTML pages into one file
        // -------------------------------------------------
        try
        {
            string[] htmlFiles = Directory.GetFiles(htmlOutputFolder, "page_*.html");
            Array.Sort(htmlFiles); // ensure correct page order

            using (var writer = new StreamWriter(combinedHtmlPath, false, Encoding.UTF8))
            {
                bool firstFile = true;
                foreach (string file in htmlFiles)
                {
                    string content = File.ReadAllText(file, Encoding.UTF8);

                    // Remove duplicate HTML wrappers for all but the first file
                    if (!firstFile)
                    {
                        content = content
                            .Replace("<html>", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("</html>", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("<head>", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("</head>", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("<body>", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("</body>", string.Empty, StringComparison.OrdinalIgnoreCase);
                    }

                    writer.WriteLine(content);
                    firstFile = false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while combining HTML pages: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Step 3: Convert the combined HTML back to PDF
        // -------------------------------------------------
        try
        {
            // Loading HTML also requires GDI+ on non‑Windows platforms
            using (var htmlDoc = new Document(combinedHtmlPath, new HtmlLoadOptions()))
            {
                htmlDoc.Save(finalPdfPath);
            }

            Console.WriteLine($"Successfully created PDF: {finalPdfPath}");
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Skipping final PDF generation.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML‑to‑PDF conversion: {ex.Message}");
        }
    }
}
