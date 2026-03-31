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

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Define permissions (allow printing and content extraction)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document with AES‑256
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF into a memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                document.Save(memoryStream);
                // Reset the stream position for reading/transmission
                memoryStream.Position = 0;

                // Example output: size of the encrypted PDF
                Console.WriteLine($"Encrypted PDF size (bytes): {memoryStream.Length}");
                // The memoryStream can now be sent over a network connection
            }
        }
    }
}