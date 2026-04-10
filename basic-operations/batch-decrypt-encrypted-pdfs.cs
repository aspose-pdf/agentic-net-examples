using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = "DecryptedPdfs";
        // Shared owner password for all PDFs
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] encryptedFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string encryptedPath in encryptedFiles)
        {
            // Build output file name (append _decrypted to avoid overwriting)
            string fileName = Path.GetFileNameWithoutExtension(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, $"{fileName}_decrypted.pdf");

            try
            {
                // Open the encrypted PDF using the owner password
                using (Document doc = new Document(encryptedPath, ownerPassword))
                {
                    // Decrypt the document (no parameters required)
                    doc.Decrypt();

                    // Save the unprotected copy
                    doc.Save(decryptedPath);
                }

                Console.WriteLine($"Decrypted: {encryptedPath} -> {decryptedPath}");
            }
            catch (InvalidPasswordException ex)
            {
                // Handle case where the provided password is incorrect
                Console.Error.WriteLine($"Invalid password for '{encryptedPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling
                Console.Error.WriteLine($"Error processing '{encryptedPath}': {ex.Message}");
            }
        }
    }
}
