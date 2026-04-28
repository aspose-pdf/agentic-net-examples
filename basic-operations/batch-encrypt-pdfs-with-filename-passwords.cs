using System;
using System.IO;
using Aspose.Pdf; // Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = "EncryptedPdfs";
        // Path to the log file that stores file‑name → password mapping
        const string logPath = "encryption_log.txt";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Append log entries; each line contains timestamp, file name (without extension) and password
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string password = GeneratePassword(baseName); // unique password per file
                string encryptedPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                try
                {
                    // Load, encrypt, and save the PDF
                    using (Document doc = new Document(pdfPath))
                    {
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                        doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);
                        doc.Save(encryptedPath); // Save encrypted document
                    }

                    // Record the password securely (plain text here for illustration)
                    logWriter.WriteLine($"{DateTime.UtcNow:u}\t{baseName}\t{password}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Simple deterministic password generator based on the file name
    static string GeneratePassword(string name)
    {
        char[] chars = name.ToCharArray();
        Array.Reverse(chars);
        return new string(chars) + "_Secure!";
    }
}
