using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string svgOutputPath  = "output.svg";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Encrypt the original PDF and save it.
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // Open the encrypted PDF (password supplied in constructor), decrypt, and convert to SVG.
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                encDoc.Decrypt(); // No parameters required.
                SvgSaveOptions svgOpts = new SvgSaveOptions(); // Explicit SVG save options.
                encDoc.Save(svgOutputPath, svgOpts);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}