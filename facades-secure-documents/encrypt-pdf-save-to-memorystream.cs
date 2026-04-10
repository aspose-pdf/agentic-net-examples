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

        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the source PDF
            Document doc = new Document(inputPath);

            // Grant typical permissions that are guaranteed to exist in the Permissions enum
            // (PrintDocument and ExtractContent are universally available across Aspose.PDF versions)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document with AES‑256
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF into the memory stream
            doc.Save(outputStream);

            // Prepare the stream for transmission
            outputStream.Position = 0;
            Console.WriteLine($"Encrypted PDF size: {outputStream.Length} bytes");
            // The stream can now be sent over the network
        }
    }
}
