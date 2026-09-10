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

        // Load the PDF, encrypt it, and save the encrypted version to a memory stream.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Define permissions (e.g., allow printing and content extraction).
            Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                          Aspose.Pdf.Permissions.ExtractContent;

            // Encrypt using AES-256 (preferred algorithm).
            doc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

            // Save the encrypted PDF into a MemoryStream for network transmission.
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                doc.Save(encryptedStream);
                encryptedStream.Position = 0; // Reset for reading elsewhere.

                // Example output: size of the encrypted PDF.
                Console.WriteLine($"Encrypted PDF size: {encryptedStream.Length} bytes");

                // The 'encryptedStream' now contains the encrypted PDF and can be sent over the network.
            }
        }
    }
}