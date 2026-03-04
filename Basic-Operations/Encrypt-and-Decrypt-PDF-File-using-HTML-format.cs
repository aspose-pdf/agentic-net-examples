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

        // Passwords for encryption/decryption
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
                // Define permissions and encryption algorithm (AES-256)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPdf); // Save the encrypted PDF
            }

            // ---------- Decrypt the PDF ----------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                encDoc.Decrypt();               // Decrypt in‑memory
                encDoc.Save(decryptedPdf);      // Save the decrypted PDF
            }

            // ---------- Convert decrypted PDF to HTML ----------
            using (Document decDoc = new Document(decryptedPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // HTML conversion relies on GDI+ (Windows only); handle gracefully on other platforms
                try
                {
                    decDoc.Save(htmlOutput, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlOutput}'.");
                }
                catch (TypeInitializationException)
                {
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