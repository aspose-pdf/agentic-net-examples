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

            // Encrypt the document with AES-256
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF to a memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                document.Save(outputStream);
                Console.WriteLine($"Encrypted PDF size: {outputStream.Length} bytes");
                // Reset position if the stream will be read/sent later
                outputStream.Position = 0;
                // Network transmission logic would use outputStream here
            }
        }
    }
}