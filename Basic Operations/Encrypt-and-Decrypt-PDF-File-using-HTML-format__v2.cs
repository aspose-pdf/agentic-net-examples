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
            // ---------- Encrypt ----------
            using (Document doc = new Document(inputPdf))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPdf);
            }

            // ---------- Decrypt ----------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                encDoc.Decrypt();                     // No parameters
                encDoc.Save(decryptedPdf);
            }

            // ---------- Convert to HTML ----------
            using (Document decDoc = new Document(decryptedPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example options; adjust as needed
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    decDoc.Save(htmlOutput, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlOutput}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only)
                    Console.WriteLine("HTML conversion is not supported on this platform. Skipped.");
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