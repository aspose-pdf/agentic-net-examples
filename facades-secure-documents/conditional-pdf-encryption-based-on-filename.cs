using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (cross‑platform safe)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        // Folder containing source PDFs (fallback to current directory if missing)
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        // Folder where encrypted PDFs will be saved
        string outputFolder = Path.Combine(baseDir, "EncryptedPdfs");

        // Validate input folder – if it does not exist, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"[Error] Input folder not found: '{inputFolder}'. Ensure PDFs are placed there.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Passwords used for encryption (could be generated per file if needed)
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Permissions to apply to all encrypted files
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"[Warning] Skipping missing file: {inputPath}");
                continue;
            }

            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Choose CryptoAlgorithm based on naming convention
            CryptoAlgorithm algorithm = ChooseAlgorithm(fileName);

            try
            {
                // Load the PDF, encrypt, and save
                using (Document pdfDoc = new Document(inputPath))
                {
                    pdfDoc.Encrypt(userPassword, ownerPassword, perms, algorithm);
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted '{fileName}.pdf' using {algorithm} → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to encrypt '{inputPath}': {ex.Message}");
            }
        }
    }

    // Determines the CryptoAlgorithm based on file name patterns
    private static CryptoAlgorithm ChooseAlgorithm(string fileName)
    {
        // Example conventions:
        // - Files containing "confidential" → strongest encryption (AESx256)
        // - Files containing "internal"     → AESx128
        // - Files containing "public"       → RC4x40 (least restrictive)
        // - Default                         → AESx128

        if (fileName.IndexOf("confidential", StringComparison.OrdinalIgnoreCase) >= 0)
            return CryptoAlgorithm.AESx256;
        if (fileName.IndexOf("internal", StringComparison.OrdinalIgnoreCase) >= 0)
            return CryptoAlgorithm.AESx128;
        if (fileName.IndexOf("public", StringComparison.OrdinalIgnoreCase) >= 0)
            return CryptoAlgorithm.RC4x40;

        return CryptoAlgorithm.AESx128;
    }
}

/*
 * NOTE:
 * The project references the NuGet package 'System.Security.Cryptography.Pkcs'.
 * Versions prior to 9.0.1 contain known vulnerabilities (NU1902, NU1903).
 * To eliminate the warnings (and potential security issues), update the package
 * reference in the .csproj file, e.g.:
 *   <PackageReference Include="System.Security.Cryptography.Pkcs" Version="9.0.1" />
 * or a later safe version.
 */