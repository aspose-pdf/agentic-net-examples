using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Determines encryption parameters based on the file name.
    // Example conventions:
    //   *AES256*  -> AES algorithm, 256‑bit key
    //   *AES128*  -> AES algorithm, 128‑bit key
    //   *RC4*     -> RC4 algorithm, 128‑bit key (default for RC4)
    //   otherwise -> AES algorithm, 256‑bit key (most secure default)
    private static (KeySize keySize, Algorithm? algorithm) GetEncryptionSettings(string fileName)
    {
        string name = Path.GetFileNameWithoutExtension(fileName).ToUpperInvariant();

        if (name.Contains("AES256"))
            return (KeySize.x256, Algorithm.AES);
        if (name.Contains("AES128"))
            return (KeySize.x128, Algorithm.AES);
        if (name.Contains("RC4"))
            return (KeySize.x128, Algorithm.RC4); // RC4 supports 40 or 128 bits; using 128 for better security
        // Default: AES‑256
        return (KeySize.x256, Algorithm.AES);
    }

    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Folder where encrypted PDFs will be written
        const string outputFolder = @"C:\PdfEncrypted";

        // Simple passwords used for demonstration – replace with real values as needed
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine encryption settings from the file name
                var (keySize, algorithm) = GetEncryptionSettings(inputPath);

                // Build output file name (same name with "_enc" suffix)
                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_enc.pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Create the PdfFileSecurity facade
                PdfFileSecurity fileSecurity = new PdfFileSecurity();

                // Bind the source PDF
                fileSecurity.BindPdf(inputPath);

                // Set desired privileges – here we allow printing only
                DocumentPrivilege privilege = DocumentPrivilege.Print;

                // Encrypt using the appropriate overload
                bool success;
                if (algorithm.HasValue)
                {
                    // Use overload that specifies the cipher algorithm
                    success = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, keySize, algorithm.Value);
                }
                else
                {
                    // Fallback to overload without explicit algorithm (defaults to AES for x128/x256)
                    success = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, keySize);
                }

                if (!success)
                {
                    Console.Error.WriteLine($"Encryption failed for '{inputPath}'.");
                    continue;
                }

                // Save the encrypted PDF
                fileSecurity.Save(outputPath);

                Console.WriteLine($"Encrypted '{Path.GetFileName(inputPath)}' -> '{outputFileName}' (KeySize: {keySize}, Algorithm: {algorithm?.ToString() ?? "Default"}).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}