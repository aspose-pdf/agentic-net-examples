using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, CryptoAlgorithm, Permissions, SvgSaveOptions)

class Program
{
    static void Main()
    {
        // Input PDF file to be encrypted
        const string inputPdfPath      = "input.pdf";
        // Encrypted PDF output (intermediate file)
        const string encryptedPdfPath  = "encrypted.pdf";
        // Final SVG output after decryption
        const string outputSvgPath     = "output.svg";

        // Passwords for encryption/decryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the original PDF and encrypt it
            // -------------------------------------------------
            using (Document doc = new Document(inputPdfPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdfPath);
            }

            // -------------------------------------------------
            // 2. Open the encrypted PDF with the user password,
            //    decrypt it, and convert to SVG
            // -------------------------------------------------
            using (Document encryptedDoc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (no parameters)
                encryptedDoc.Decrypt();

                // Prepare SVG save options (required for non‑PDF output)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save as SVG
                encryptedDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
            Console.WriteLine($"Encrypted PDF: {encryptedPdfPath}");
            Console.WriteLine($"SVG output   : {outputSvgPath}");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}