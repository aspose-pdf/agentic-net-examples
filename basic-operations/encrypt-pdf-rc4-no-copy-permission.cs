using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt ----------
        // Load the source PDF, apply RC4 encryption and allow only printing (no copy permission)
        using (Document doc = new Document(inputPath))
        {
            // Permissions: allow printing, do NOT include ExtractContent (copy) permission
            Permissions perms = Permissions.PrintDocument;

            // RC4 128‑bit algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // ---------- Verify security settings ----------
        // Open the encrypted PDF with the user password
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Document.Permissions returns an int, cast it to Permissions enum for bitwise checks
            Permissions currentPerms = (Permissions)encDoc.Permissions;

            // The ExtractContent flag (copy permission) should NOT be set
            bool copyAllowed = (currentPerms & Permissions.ExtractContent) == Permissions.ExtractContent;

            Console.WriteLine($"Copy permission enabled: {copyAllowed}");
            // Expected output: Copy permission enabled: False
        }
    }
}
