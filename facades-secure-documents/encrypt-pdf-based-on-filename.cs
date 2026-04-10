using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Determines the encryption settings based on the file name.
    static void GetEncryptionSettings(string fileName, out KeySize keySize, out Algorithm algorithm)
    {
        // Default to AES 256 if no specific marker is found.
        keySize = KeySize.x256;
        algorithm = Algorithm.AES;

        string name = Path.GetFileNameWithoutExtension(fileName).ToUpperInvariant();

        if (name.Contains("AES256"))
        {
            keySize = KeySize.x256;
            algorithm = Algorithm.AES;
        }
        else if (name.Contains("AES128"))
        {
            keySize = KeySize.x128;
            algorithm = Algorithm.AES;
        }
        else if (name.Contains("RC4_40"))
        {
            keySize = KeySize.x40;
            algorithm = Algorithm.RC4;
        }
        else if (name.Contains("RC4_128"))
        {
            keySize = KeySize.x128;
            algorithm = Algorithm.RC4;
        }
        // Add more patterns as needed.
    }

    static void Main()
    {
        // Example input files – in a real scenario you might enumerate a directory.
        string[] inputFiles = {
            @"C:\Docs\report_AES256.pdf",
            @"C:\Docs\summary_AES128.pdf",
            @"C:\Docs\confidential_RC4_40.pdf",
            @"C:\Docs\public_RC4_128.pdf"
        };

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Determine output path – same folder, suffix "_encrypted".
            string dir = Path.GetDirectoryName(inputPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(dir, $"{nameWithoutExt}_encrypted.pdf");

            // Resolve encryption parameters from the file name.
            GetEncryptionSettings(inputPath, out KeySize keySize, out Algorithm algorithm);

            // Choose a privilege – here we allow printing only.
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Use PdfFileSecurity facade: create, bind (load), encrypt, save.
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                // Load the source PDF.
                fileSecurity.BindPdf(inputPath);

                // Encrypt with the selected algorithm.
                // The overload with Algorithm is used to specify AES or RC4 explicitly.
                bool success = fileSecurity.EncryptFile(
                    userPassword,
                    ownerPassword,
                    privilege,
                    keySize,
                    algorithm);

                if (!success)
                {
                    Console.Error.WriteLine($"Encryption failed for: {inputPath}");
                    continue;
                }

                // Save the encrypted PDF.
                fileSecurity.Save(outputPath);
            }

            Console.WriteLine($"Encrypted '{inputPath}' → '{outputPath}' using {algorithm} with key size {keySize}");
        }
    }
}