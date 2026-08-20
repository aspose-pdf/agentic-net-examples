using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted copies will be saved
        const string outputFolder = "DecryptedPdfs";
        // Shared owner password for all PDFs in the batch
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string encryptedPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, $"{fileName}_decrypted.pdf");

            try
            {
                // Open the encrypted document using the owner password
                using (Document doc = new Document(encryptedPath, ownerPassword))
                {
                    // Remove encryption
                    doc.Decrypt();

                    // Save the unprotected copy
                    doc.Save(decryptedPath);
                }

                Console.WriteLine($"Decrypted: {encryptedPath} → {decryptedPath}");
            }
            catch (InvalidPasswordException ex)
            {
                // Owner password was incorrect or the file is not encrypted
                Console.Error.WriteLine($"Invalid password for '{encryptedPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Any other error (e.g., file access issues)
                Console.Error.WriteLine($"Error processing '{encryptedPath}': {ex.Message}");
            }
        }
    }
}