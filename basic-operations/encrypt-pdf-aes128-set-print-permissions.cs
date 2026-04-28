using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Security;    // CryptoAlgorithm and Permissions enums are in Aspose.Pdf

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Define permissions: allow printing and high‑quality printing
        Permissions perms = Permissions.PrintDocument | Permissions.PrintingQuality;

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Apply AES‑128 encryption with the specified permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

                // Save the encrypted document
                doc.Save(outputPath);
            }

            // Attempt to open the encrypted PDF with the user password to verify it was encrypted correctly
            using (Document encryptedDoc = new Document(outputPath, userPassword))
            {
                // If we reach this point, the document was successfully opened with the password
                Console.WriteLine("Encryption applied successfully.");
                Console.WriteLine($"Permissions set: {perms}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}