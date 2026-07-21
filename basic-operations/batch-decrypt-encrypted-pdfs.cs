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
        // Shared owner password for all PDFs in the batch
        const string ownerPassword = "ownerPassword123";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the encrypted document using the owner password
                using (Document doc = new Document(inputPath, ownerPassword))
                {
                    // Remove encryption
                    doc.Decrypt();

                    // Save an unprotected copy
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Decrypted: {fileName} → {outputPath}");
            }
            catch (InvalidPasswordException ex)
            {
                Console.Error.WriteLine($"Invalid password for file '{fileName}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{fileName}': {ex.Message}");
            }
        }
    }
}
