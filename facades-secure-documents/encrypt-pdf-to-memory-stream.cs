using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the permissions you want to grant. Use only members that exist in the Permissions enum.
            // Example: allow printing and content extraction (you can add other valid flags as needed).
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the PDF using AES‑128 (CryptoAlgorithm.AESx128) and the supplied passwords.
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF into a memory stream for network transmission.
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                doc.Save(encryptedStream);
                encryptedStream.Position = 0; // Reset for reading later.

                Console.WriteLine($"Encrypted PDF size: {encryptedStream.Length} bytes");
                // The stream can now be sent over a network connection.
            }
        }
    }
}
