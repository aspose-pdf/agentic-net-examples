using System;
using System.IO;
using Aspose.Pdf;

class BatchEncrypt
{
    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfEncrypted";
        // Path to the password log file (stored securely by the caller)
        const string logPath = @"C:\PdfEncrypted\passwords.log";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the log file for appending
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            // Process each PDF file in the input folder
            foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Generate a unique password from the file name (e.g., base name + a fixed salt)
                string fileName = Path.GetFileNameWithoutExtension(pdfFile);
                string password = GeneratePassword(fileName);

                // Define output file path
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                // Encrypt the PDF using a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Set permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with AES-256 algorithm
                    doc.Encrypt(userPassword: password,
                                ownerPassword: password,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                // Log the file name and its password
                logWriter.WriteLine($"{fileName}: {password}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Simple deterministic password generator based on file name
    private static string GeneratePassword(string baseName)
    {
        // Example: reverse the base name and append a constant suffix
        const string suffix = "_Secure2026";
        char[] chars = baseName.ToCharArray();
        Array.Reverse(chars);
        return new string(chars) + suffix;
    }
}