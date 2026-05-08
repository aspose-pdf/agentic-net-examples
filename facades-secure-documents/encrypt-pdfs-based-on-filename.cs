using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string inputDir = "InputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Directory not found: {inputDir}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string inFile in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string nameLower = Path.GetFileNameWithoutExtension(inFile).ToLowerInvariant();

            // Default encryption settings (AES‑256, allow printing)
            Algorithm algorithm = Algorithm.AES;
            KeySize keySize = KeySize.x256;
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Adjust settings based on naming conventions
            if (nameLower.Contains("rc4"))
            {
                algorithm = Algorithm.RC4;
                if (nameLower.Contains("256"))
                    keySize = KeySize.x256; // will be corrected later (invalid)
                else if (nameLower.Contains("128"))
                    keySize = KeySize.x128;
                else if (nameLower.Contains("40"))
                    keySize = KeySize.x40;
                else
                    keySize = KeySize.x128;
            }
            else if (nameLower.Contains("aes"))
            {
                algorithm = Algorithm.AES;
                if (nameLower.Contains("256"))
                    keySize = KeySize.x256;
                else if (nameLower.Contains("128"))
                    keySize = KeySize.x128;
                else
                    keySize = KeySize.x128;
            }

            // Ensure only valid algorithm/keySize combinations
            if (algorithm == Algorithm.AES && keySize == KeySize.x40)
                keySize = KeySize.x128; // AES does not support 40‑bit keys
            if (algorithm == Algorithm.RC4 && keySize == KeySize.x256)
                keySize = KeySize.x128; // RC4 does not support 256‑bit keys

            // Output file name (original name + "_encrypted")
            string outFile = Path.Combine(
                Path.GetDirectoryName(inFile)!,
                $"{Path.GetFileNameWithoutExtension(inFile)}_encrypted.pdf");

            // Encrypt using PdfFileSecurity (facade API)
            using (PdfFileSecurity security = new PdfFileSecurity(inFile, outFile))
            {
                // Empty passwords are allowed; owner password will be generated automatically
                string userPassword = "";
                string ownerPassword = "";

                bool success = security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    privilege,
                    keySize,
                    algorithm);

                if (success)
                {
                    Console.WriteLine($"Encrypted '{inFile}' → '{outFile}' using {algorithm} {keySize}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to encrypt '{inFile}'.");
                }
            }
        }
    }
}