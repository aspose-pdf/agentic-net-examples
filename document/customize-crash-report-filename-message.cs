using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string htmlOutputPath = "output.html";

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Encrypt ----------
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
                Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
            }

            // ---------- Decrypt ----------
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                encDoc.Decrypt(); // No parameters
                encDoc.Save(decryptedPath);
                Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
            }

            // ---------- Extract text ----------
            using (Document decDoc = new Document(decryptedPath))
            {
                TextAbsorber absorber = new TextAbsorber();
                decDoc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text.Length}");
            }

            // ---------- Save as HTML (Windows only) ----------
            using (Document htmlDoc = new Document(decryptedPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    htmlDoc.Save(htmlOutputPath, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{htmlOutputPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}