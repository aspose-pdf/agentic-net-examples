using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "changedEncryptRC4.pdf";
        const string oldOwnerPassword = "ownerOld";
        const string newUserPassword = "userNew2";
        const string newOwnerPassword = "ownerNew2";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF using the known owner password
            using (Document doc = new Document(inputPath, oldOwnerPassword))
            {
                // Define the permissions you want to allow after re‑encryption
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt the document with the new user/owner passwords and RC4‑128 algorithm
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
