using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum lives here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF protected with owner password
        const string outputPath = "encrypted.pdf";  // Resulting encrypted file
        const string ownerPassword = "ownerPass";   // Owner password to open the source PDF
        const string userPassword  = "userPass";    // User password for the new encryption

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF using the owner password (full access)
        using (Document doc = new Document(inputPath, ownerPassword))
        {
            // No permissions – forbid all operations
            Permissions perms = (Permissions)0; // equivalent to "no permissions"

            // Encrypt with user & owner passwords, no permissions, using AES‑256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the newly encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
