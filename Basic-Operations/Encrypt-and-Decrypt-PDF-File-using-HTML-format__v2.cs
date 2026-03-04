using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string encryptedPdf  = "encrypted.pdf";
        const string decryptedPdf  = "decrypted.pdf";
        const string htmlOutput    = "output.html";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // ---------- Encrypt the PDF ----------
            using (Document doc = new Document(inputPdf))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            // ---------- Decrypt the PDF ----------
            // Open the encrypted PDF with the user password
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Decrypt the document (no parameters)
                encDoc.Decrypt();

                // Save the decrypted PDF
                encDoc.Save(decryptedPdf);
            }

            // ---------- Convert the decrypted PDF to HTML ----------
            using (Document htmlDoc = new Document(decryptedPdf))
            {
                // Configure HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle gracefully on other platforms
                try
                {
                    htmlDoc.Save(htmlOutput, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlOutput}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }

            Console.WriteLine("Encryption, decryption, and HTML conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}