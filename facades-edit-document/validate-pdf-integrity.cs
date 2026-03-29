using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string editedPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Compute checksum of the original PDF
        byte[] originalHash = ComputeSha256(inputPath);
        string originalHashString = BitConverter.ToString(originalHash).Replace("-", string.Empty);
        Console.WriteLine($"Original SHA-256: {originalHashString}");

        // Load the PDF, apply a simple modification, and save it
        using (Document doc = new Document(inputPath))
        {
            // Simple modification: add a blank page at the end
            doc.Pages.Add();
            doc.Save(editedPath);
        }

        // Compute checksum of the edited PDF
        byte[] editedHash = ComputeSha256(editedPath);
        string editedHashString = BitConverter.ToString(editedHash).Replace("-", string.Empty);
        Console.WriteLine($"Edited SHA-256: {editedHashString}");

        // Compare the two hashes
        bool isSame = true;
        if (originalHash.Length != editedHash.Length)
        {
            isSame = false;
        }
        else
        {
            for (int i = 0; i < originalHash.Length; i++)
            {
                if (originalHash[i] != editedHash[i])
                {
                    isSame = false;
                    break;
                }
            }
        }

        Console.WriteLine(isSame ? "PDF unchanged." : "PDF modified.");
    }

    private static byte[] ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(stream);
        }
    }
}