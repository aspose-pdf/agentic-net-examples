using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is required for PdfFileSecurity if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "protected.pdf";
        const string outputPath = "updated.pdf";

        // Original owner password of the protected PDF
        const string ownerPassword = "owner123";

        // New passwords to be set after re‑encryption (can be the same as original)
        const string newUserPassword = "newuser";
        const string newOwnerPassword = "newowner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF using the owner password
        using (Document doc = new Document(inputPath, ownerPassword))
        {
            // Decrypt the document in memory
            doc.Decrypt();

            // Update the Creator metadata (DocumentInfo.Creator is the correct property)
            doc.Info.Creator = "MyCreatorTool";

            // Define desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Re‑encrypt the document using AES‑256 (preferred algorithm)
            doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified and re‑encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}