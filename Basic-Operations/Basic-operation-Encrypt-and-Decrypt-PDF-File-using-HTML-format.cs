using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string htmlOutputPath = "output.html";

        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt ----------
        using (Document doc = new Document(inputPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with AES‑256 (preferred algorithm)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // ---------- Decrypt ----------
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt – no parameters required
            encDoc.Decrypt();

            // Save the decrypted PDF (optional intermediate step)
            encDoc.Save(decryptedPath);
        }

        // ---------- Convert to HTML ----------
        using (Document decDoc = new Document(decryptedPath))
        {
            // HTML conversion requires GDI+; wrap in try‑catch for non‑Windows platforms
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Store raster images as PNGs embedded in SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                decDoc.Save(htmlOutputPath, htmlOpts);
                Console.WriteLine($"HTML file created: {htmlOutputPath}");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}