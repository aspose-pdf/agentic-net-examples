using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes SvgSaveOptions, CryptoAlgorithm, Permissions

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string encryptedPdf  = "encrypted.pdf";
        const string outputSvg     = "output.svg";
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
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES-256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            // ---------- Decrypt and convert to SVG ----------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Decrypt the document (no parameters)
                encDoc.Decrypt();

                // Prepare SVG save options (default constructor is sufficient for most cases)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save as SVG; explicit SaveOptions ensures non‑PDF output
                encDoc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}