using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "encrypted";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = "decrypted";
        // Shared owner password for all PDFs in the batch
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);
            string outPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the encrypted PDF with the owner password
                using (Document doc = new Document(filePath, ownerPassword))
                {
                    // Remove encryption
                    doc.Decrypt();
                    // Save an unprotected copy
                    doc.Save(outPath);
                }

                Console.WriteLine($"Decrypted: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to decrypt {fileName}: {ex.Message}");
            }
        }
    }
}