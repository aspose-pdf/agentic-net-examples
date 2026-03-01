using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath      = "input.pdf";
        // Encrypted PDF file
        const string encryptedPdfPath  = "encrypted.pdf";
        // Output HTML file (result of decryption + conversion)
        const string outputHtmlPath    = "output.html";

        // Passwords for encryption/decryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Encrypt the PDF
            // -------------------------------------------------
            using (Document doc = new Document(inputPdfPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES‑256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted document (PDF format)
                doc.Save(encryptedPdfPath);
            }

            // -------------------------------------------------
            // 2. Decrypt and convert to HTML
            // -------------------------------------------------
            // Open the encrypted PDF supplying the user password
            using (Document encryptedDoc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (no parameters)
                encryptedDoc.Decrypt();

                // Prepare HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example: embed all resources into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Example: save raster images as PNG embedded in SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ and works only on Windows.
                // Wrap the call in a try‑catch to avoid crashes on macOS/Linux.
                try
                {
                    encryptedDoc.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"Decrypted PDF saved as HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
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