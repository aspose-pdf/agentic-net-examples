using System;
using System.IO;
using Aspose.Pdf;               // Document, Permissions, CryptoAlgorithm, SvgSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // original PDF
        const string encryptedPdf  = "encrypted.pdf";      // encrypted version
        const string decryptedPdf  = "decrypted.pdf";      // decrypted version
        const string outputSvg     = "output.svg";         // final SVG file
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // 1. Encrypt the original PDF and save it
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Set permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES‑256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            // -----------------------------------------------------------------
            // 2. Open the encrypted PDF, decrypt it, and save the clear PDF
            // -----------------------------------------------------------------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Decrypt (no parameters)
                encDoc.Decrypt();

                // Save the decrypted PDF
                encDoc.Save(decryptedPdf);
            }

            // -----------------------------------------------------------------
            // 3. Convert the decrypted PDF to SVG format
            // -----------------------------------------------------------------
            using (Document decDoc = new Document(decryptedPdf))
            {
                // SVG save options (default constructor is sufficient)
                SvgSaveOptions svgOpts = new SvgSaveOptions();

                // Save as SVG – must pass SaveOptions explicitly, otherwise a PDF is written
                decDoc.Save(outputSvg, svgOpts);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
            Console.WriteLine($"Encrypted PDF : {encryptedPdf}");
            Console.WriteLine($"Decrypted PDF : {decryptedPdf}");
            Console.WriteLine($"SVG output    : {outputSvg}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}