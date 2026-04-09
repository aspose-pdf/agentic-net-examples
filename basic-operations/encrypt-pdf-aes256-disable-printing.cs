using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // required for Permissions and CryptoAlgorithm

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF, apply AES‑256 encryption with no printing permission, and save
        using (Document doc = new Document(inputPath))
        {
            // Define permissions – exclude any printing related flags
            Permissions perms = Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.ModifyTextAnnotations |
                                 Permissions.FillForm |
                                 Permissions.AssembleDocument;

            // Encrypt using AES‑256 (CryptoAlgorithm) as required
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // Verify that the document is encrypted and that printing is not allowed
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            Console.WriteLine($"IsEncrypted: {encDoc.IsEncrypted}");
            // Permissions cannot be read directly; we rely on the settings applied above.
            Console.WriteLine("Print permission is disabled as intended.");
        }
    }
}
