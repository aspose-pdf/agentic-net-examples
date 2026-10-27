using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = "EncryptedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build a password from the file name and current UTC date (yyyyMMdd)
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
                string rawPassword = $"{fileName}_{datePart}";
                string password = GenerateHashPassword(rawPassword);

                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Define desired permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256; user and owner passwords are the same derived password
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: '{pdfPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Generates a 16‑character password from a SHA‑256 hash of the input string
    private static string GenerateHashPassword(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Convert to hex and take the first 16 characters for a manageable password length
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length && sb.Length < 16; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}