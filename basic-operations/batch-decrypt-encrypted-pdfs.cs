using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the encrypted PDFs
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

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(inputPath);
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

                Console.WriteLine($"Decrypted: {fileName}");
            }
            catch (InvalidPasswordException ex)
            {
                // The provided password was not valid for this file
                Console.Error.WriteLine($"Invalid password for {fileName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Any other I/O or processing error
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}