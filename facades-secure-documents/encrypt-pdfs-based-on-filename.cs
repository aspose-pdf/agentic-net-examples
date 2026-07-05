using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple method to decide encryption settings based on file name
    static void GetEncryptionSettings(string fileName, out KeySize keySize, out Algorithm algorithm)
    {
        // Example naming convention:
        //   * "_aes256"  -> AES with 256‑bit key
        //   * "_aes128"  -> AES with 128‑bit key
        //   * "_rc4"     -> RC4 with 128‑bit key (default for RC4)
        // If none match, fall back to AES‑128.
        string lower = fileName.ToLowerInvariant();

        if (lower.Contains("_aes256"))
        {
            keySize = KeySize.x256;
            algorithm = Algorithm.AES;
        }
        else if (lower.Contains("_aes128"))
        {
            keySize = KeySize.x128;
            algorithm = Algorithm.AES;
        }
        else if (lower.Contains("_rc4"))
        {
            keySize = KeySize.x128; // RC4 supports 40‑bit and 128‑bit; use 128‑bit for better security
            algorithm = Algorithm.RC4;
        }
        else
        {
            // Default encryption: AES‑128
            keySize = KeySize.x128;
            algorithm = Algorithm.AES;
        }
    }

    static void Main()
    {
        // Input directory containing PDFs to encrypt
        const string inputDir = @"C:\InputPdfs";
        // Output directory for encrypted PDFs
        const string outputDir = @"C:\EncryptedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Passwords used for encryption – could be generated or read from a secure source
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Iterate over all PDF files in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, fileName + "_encrypted.pdf");

            // Determine encryption parameters based on naming convention
            GetEncryptionSettings(fileName, out KeySize keySize, out Algorithm algorithm);

            // Create the PdfFileSecurity facade with source and destination files
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Set desired privileges – here we allow printing only
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Perform encryption using the selected key size and algorithm
            bool success = security.EncryptFile(userPassword, ownerPassword, privilege, keySize, algorithm);

            if (!success)
            {
                Console.Error.WriteLine($"Encryption failed for '{inputPath}'.");
            }
            else
            {
                Console.WriteLine($"Encrypted '{inputPath}' -> '{outputPath}' using {algorithm} with {keySize}.");
            }

            // No need to dispose; PdfFileSecurity does not implement IDisposable
        }
    }
}