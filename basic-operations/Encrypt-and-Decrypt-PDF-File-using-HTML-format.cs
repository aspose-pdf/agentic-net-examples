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
                // Define permissions and encryption algorithm
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPdf); // Save encrypted PDF
            }

            // ---------- Decrypt the PDF ----------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                encDoc.Decrypt();               // Decrypt in‑memory
                encDoc.Save(decryptedPdf);      // Save decrypted PDF
            }

            // ---------- Convert decrypted PDF to HTML ----------
            using (Document htmlDoc = new Document(decryptedPdf))
            {
                // HTML conversion requires GDI+ (Windows only)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    htmlDoc.Save(htmlOutput, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlOutput}'.");
                }
                catch (TypeInitializationException)
                {
                    // GDI+ not available on non‑Windows platforms
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }

            Console.WriteLine("Encryption, decryption, and HTML conversion completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}