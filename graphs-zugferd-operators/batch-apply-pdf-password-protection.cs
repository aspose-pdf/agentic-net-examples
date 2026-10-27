using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\PdfSource";
        // Folder where encrypted PDFs will be written
        const string outputFolder = @"C:\PdfEncrypted";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Passwords to apply to every PDF
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Permissions to grant after encryption (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string encryptedPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF, encrypt it, and save the encrypted version
                using (Document doc = new Document(pdfPath))
                {
                    // Apply encryption using AES-256 (recommended)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(encryptedPath); // Save writes PDF regardless of extension
                }

                Console.WriteLine($"Encrypted: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}