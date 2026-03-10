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
        const string outputSvg     = "output.svg";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // ---------- Encrypt the PDF ----------
            using (Document doc = new Document(inputPdf))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted document
                doc.Save(encryptedPdf);
            }

            // ---------- Decrypt the PDF ----------
            // Open the encrypted file using the user password
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Decrypt (no parameters)
                encDoc.Decrypt();

                // Save the decrypted version
                encDoc.Save(decryptedPdf);
            }

            // ---------- Convert the decrypted PDF to SVG ----------
            using (Document decDoc = new Document(decryptedPdf))
            {
                // SvgSaveOptions is required for non‑PDF output
                SvgSaveOptions svgOpts = new SvgSaveOptions();

                // Save as SVG
                decDoc.Save(outputSvg, svgOpts);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}