using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder containing PDFs to encrypt (created if missing)
        string inputDir = Path.Combine(baseDir, "InputPdfs");
        // Output folder for encrypted PDFs (created if missing)
        string outputDir = Path.Combine(baseDir, "EncryptedPdfs");
        // Secure log file that records filename and generated password
        string logPath = Path.Combine(baseDir, "encryption_log.txt");

        // Ensure the required directories exist
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Place PDFs there and rerun the program.");
            return;
        }

        // Open the log file once and keep it open for the batch
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true, Encoding.UTF8))
        {
            foreach (string pdfPath in pdfFiles)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    // Generate a deterministic password from the file name
                    string password = GeneratePasswordFromFileName(fileName);

                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                    // Encrypt the PDF using AES‑256 and basic permissions
                    using (Document doc = new Document(pdfPath))
                    {
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                        doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);
                        doc.Save(outputPath);
                    }

                    // Record the mapping in the secure log
                    logWriter.WriteLine($"{Path.GetFileName(pdfPath)}\t{password}");
                    Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)}");
                }
                catch (Exception ex)
                {
                    // Log the error but continue processing the remaining files
                    Console.Error.WriteLine($"[Error] Failed to process '{Path.GetFileName(pdfPath)}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Creates a 16‑character password from the SHA‑256 hash of the file name
    static string GeneratePasswordFromFileName(string name)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 8; i++) // first 8 bytes => 16 hex chars
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }
    }
}
