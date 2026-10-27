using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder  = @"C:\EncryptedPdfs";
        // Folder where decrypted copies will be saved
        const string outputFolder = @"C:\DecryptedPdfs";
        // Shared owner password for all PDFs in the batch
        const string ownerPassword = "ownerSecret";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string encryptedPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, $"{fileName}_decrypted.pdf");

            try
            {
                // Open the encrypted PDF using the owner password
                using (Document doc = new Document(encryptedPath, ownerPassword))
                {
                    // Remove encryption – Decrypt() takes no parameters
                    doc.Decrypt();

                    // Save the unprotected copy
                    doc.Save(decryptedPath);
                }

                Console.WriteLine($"Decrypted: {encryptedPath} → {decryptedPath}");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid password for file: {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{encryptedPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch decryption completed.");
    }
}