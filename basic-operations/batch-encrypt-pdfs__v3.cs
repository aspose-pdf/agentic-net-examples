using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input folder, output folder and log file can be passed as arguments.
        string inputFolder  = args.Length > 0 ? args[0] : "input-pdfs";
        string outputFolder = args.Length > 1 ? args[1] : "encrypted-pdfs";
        string logFilePath  = args.Length > 2 ? args[2] : "password-log.txt";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (StreamWriter logWriter = new StreamWriter(logFilePath, false, Encoding.UTF8))
        {
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string userPassword  = GeneratePassword(fileNameWithoutExt);
                string ownerPassword = userPassword; // using same password for simplicity

                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                using (Document doc = new Document(pdfPath))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(outputPath);
                }

                logWriter.WriteLine($"{Path.GetFileName(pdfPath)}\tUserPassword:{userPassword}\tOwnerPassword:{ownerPassword}");
                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)}");
            }
        }

        Console.WriteLine("Batch encryption completed. Passwords stored in " + logFilePath);
    }

    private static string GeneratePassword(string seed)
    {
        // Derive a deterministic password from the file name using SHA‑256 and truncate to 16 hex characters.
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(seed));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++) // 8 bytes = 16 hex chars
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}