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
                doc.Save(encryptedPdf); // Save encrypted PDF
            }

            // ---------- Decrypt ----------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                encDoc.Decrypt();                     // Remove encryption
                encDoc.Save(decryptedPdf);            // Save decrypted PDF (optional)
                
                // ---------- Convert to SVG ----------
                SvgSaveOptions svgOpts = new SvgSaveOptions(); // default options
                encDoc.Save(outputSvg, svgOpts);      // Export to SVG
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}