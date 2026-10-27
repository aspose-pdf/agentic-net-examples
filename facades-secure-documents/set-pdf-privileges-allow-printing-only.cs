using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // for CryptoAlgorithm enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define permissions: allow printing, disallow copying (extracting content)
        Permissions permissions = Permissions.PrintDocument; // only printing is permitted

        // Load the PDF, apply encryption with the desired permissions, and save the result.
        // An empty user password means the document can be opened without a password,
        // while the owner password protects the permissions.
        Document pdfDocument = new Document(inputPath);
        pdfDocument.Encrypt(
            userPassword: "",               // no password required to open
            ownerPassword: ownerPassword,   // owner password to modify permissions
            permissions: permissions,
            cryptoAlgorithm: CryptoAlgorithm.AESx256); // strong encryption
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Document privileges set and saved to '{outputPath}'.");
    }
}
