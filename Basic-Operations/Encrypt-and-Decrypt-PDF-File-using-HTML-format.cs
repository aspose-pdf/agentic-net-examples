using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf      = "input.pdf";
        const string encryptedPdf  = "encrypted.pdf";
        const string decryptedPdf  = "decrypted.pdf";
        const string htmlOutput    = "output.html";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // 1. Encrypt the PDF
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Define permissions and encryption algorithm (AES‑256 recommended)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(encryptedPdf);
        }

        // -------------------------------------------------
        // 2. Decrypt the PDF
        // -------------------------------------------------
        using (Document encDoc = new Document(encryptedPdf, userPassword))
        {
            // Decrypt (no parameters required)
            encDoc.Decrypt();

            // Save the decrypted PDF
            encDoc.Save(decryptedPdf);
        }

        // -------------------------------------------------
        // 3. Convert the decrypted PDF to HTML (Windows‑only)
        // -------------------------------------------------
        using (Document decDoc = new Document(decryptedPdf))
        {
            // HtmlSaveOptions must be supplied; otherwise a PDF is written despite the .html extension
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                // Save as HTML using the explicit options
                decDoc.Save(htmlOutput, htmlOpts);
                Console.WriteLine($"HTML saved to '{htmlOutput}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and fails on non‑Windows platforms
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}