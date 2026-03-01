using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For HtmlSaveOptions (also in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPdf   = "encrypted_input.pdf";   // Encrypted PDF file
        const string password   = "userPassword";          // User/owner password
        const string outputHtml = "output.html";           // Desired HTML output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password constructor.
            using (Document pdfDoc = new Document(inputPdf, password))
            {
                // Optional: decrypt the document before further processing.
                pdfDoc.Decrypt();

                // Prepare HTML save options (required to actually produce HTML).
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (fonts, images, CSS) into the single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Use PNG images embedded into SVG for raster images.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML. Wrap in try‑catch because HTML conversion
                // depends on GDI+ and fails on non‑Windows platforms.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}