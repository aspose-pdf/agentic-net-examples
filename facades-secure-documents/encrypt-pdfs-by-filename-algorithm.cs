using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be encrypted
        const string inputFolder = @"C:\PdfInput";
        // Output folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfEncrypted";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Passwords used for encryption (can be customized per file if needed)
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Determine encryption algorithm and key size based on naming convention
            // Example conventions:
            //   * "..._AES256_..."  -> AES algorithm with 256‑bit key
            //   * "..._AES128_..."  -> AES algorithm with 128‑bit key
            //   * "..._RC4_..."     -> RC4 algorithm with 128‑bit key
            // If none match, default to AES‑256.
            Algorithm algorithm = Algorithm.AES;
            KeySize keySize = KeySize.x256;

            if (fileName.IndexOf("AES256", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                algorithm = Algorithm.AES;
                keySize = KeySize.x256;
            }
            else if (fileName.IndexOf("AES128", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                algorithm = Algorithm.AES;
                keySize = KeySize.x128;
            }
            else if (fileName.IndexOf("RC4", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                algorithm = Algorithm.RC4;
                // RC4 supports 40‑bit and 128‑bit keys; 128‑bit is more secure.
                keySize = KeySize.x128;
            }

            try
            {
                // Initialise the facade with source and destination files
                PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

                // Encrypt the PDF using the selected algorithm and key size.
                // DocumentPrivilege.Print allows printing; adjust as required.
                bool success = fileSecurity.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    keySize,
                    algorithm);

                if (!success)
                {
                    Console.Error.WriteLine($"Encryption failed for '{inputPath}'.");
                }
                else
                {
                    Console.WriteLine($"Encrypted '{inputPath}' -> '{outputPath}' using {algorithm} ({keySize}).");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}