using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Note: CryptoAlgorithm and Permissions are in Aspose.Pdf namespace; this using is optional but safe.

class BatchPdfEncryptor
{
    // Entry point
    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfEncrypted";
        // Path to the password log file
        const string logFilePath = @"C:\PdfEncrypted\passwords.log";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the log file for appending
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true))
        {
            // Iterate over all PDF files in the input folder
            foreach (string inputFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Derive a password from the file name (without extension)
                string fileName = Path.GetFileNameWithoutExtension(inputFilePath);
                // Example: use the file name reversed as the password
                string userPassword = ReverseString(fileName);
                // Owner password can be a fixed strong password or another derived value
                string ownerPassword = "Owner_" + Guid.NewGuid().ToString("N");

                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Path for the encrypted output file
                string outputFilePath = Path.Combine(outputFolder, Path.GetFileName(inputFilePath));

                // Encrypt the PDF using a using block for deterministic disposal
                using (Document doc = new Document(inputFilePath))
                {
                    // Apply encryption with AES-256
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    // Save the encrypted document
                    doc.Save(outputFilePath);
                }

                // Log the file name and its user password (owner password is not logged for security)
                logWriter.WriteLine($"{Path.GetFileName(inputFilePath)}\t{userPassword}");
                Console.WriteLine($"Encrypted: {Path.GetFileName(inputFilePath)}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Helper method to reverse a string (simple deterministic transformation)
    private static string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}