using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for text-related classes (if needed)

// This example demonstrates how to open a password‑protected PDF file
// and convert it to HTML format using Aspose.Pdf for .NET.
class Program
{
    static void Main()
    {
        const string inputPdf   = "encrypted_input.pdf";   // Encrypted PDF file
        const string userPwd    = "userPassword";          // User (or owner) password
        const string outputHtml = "output.html";           // Desired HTML output

        // Verify that the source file exists before proceeding.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the (filename, password) constructor.
            // This follows the documented API for encrypted documents.
            using (Document pdfDoc = new Document(inputPdf, userPwd))
            {
                // Prepare HTML conversion options. HtmlSaveOptions must be supplied;
                // otherwise Document.Save(string) would produce a PDF regardless of the .html extension.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (fonts, images, CSS) into the single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Save raster images as PNG embedded into SVG (cross‑platform safe option).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ and is Windows‑only.
                // Wrap the save call in a try‑catch to handle TypeInitializationException on non‑Windows platforms.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
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