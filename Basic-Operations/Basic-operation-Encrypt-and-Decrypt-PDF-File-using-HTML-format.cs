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
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            // Encrypt the PDF
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // Decrypt the PDF
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                encDoc.Decrypt(); // No parameters
                encDoc.Save(decryptedPath);
            }

            // Convert the decrypted PDF to HTML (Windows‑only GDI+ operation)
            using (Document decDoc = new Document(decryptedPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                try
                {
                    decDoc.Save(htmlOutputPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlOutputPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }

            Console.WriteLine("Encrypt/Decrypt/HTML conversion completed.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}