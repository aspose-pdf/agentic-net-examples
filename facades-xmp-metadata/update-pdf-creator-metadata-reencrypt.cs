using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated_encrypted.pdf";
        const string ownerPassword = "owner123";      // original owner password
        const string newUserPassword = "newuser";    // new user password (can be empty)
        const string newOwnerPassword = "newowner";  // new owner password (can be empty)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF using the owner password
        using (Document doc = new Document(inputPath, ownerPassword))
        {
            // Remove existing encryption
            doc.Decrypt();

            // Update the Creator metadata (the correct property name)
            doc.Info.Creator = "MyCreatorTool";

            // Re‑encrypt the document with desired permissions and AES‑256 encryption
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified and re‑encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}