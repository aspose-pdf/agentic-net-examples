using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the encryption scenarios to demonstrate.
        // DocumentPrivilege.Print is used as an example; any other privilege can be chosen.
        var scenarios = new[]
        {
            // 40‑bit RC4 (KeySize.x40, Algorithm.RC4) – low security, legacy PDFs.
            new { Output = "encrypted_rc4_40.pdf", Privilege = DocumentPrivilege.Print, Key = KeySize.x40, Algo = Algorithm.RC4 },

            // 128‑bit RC4 (KeySize.x128, Algorithm.RC4) – stronger RC4.
            new { Output = "encrypted_rc4_128.pdf", Privilege = DocumentPrivilege.Print, Key = KeySize.x128, Algo = Algorithm.RC4 },

            // 128‑bit AES (KeySize.x128, Algorithm.AES) – modern AES encryption.
            new { Output = "encrypted_aes_128.pdf", Privilege = DocumentPrivilege.Print, Key = KeySize.x128, Algo = Algorithm.AES },

            // 256‑bit AES (KeySize.x256, Algorithm.AES) – strongest supported encryption.
            new { Output = "encrypted_aes_256.pdf", Privilege = DocumentPrivilege.Print, Key = KeySize.x256, Algo = Algorithm.AES }
        };

        foreach (var s in scenarios)
        {
            // Create the security facade.
            PdfFileSecurity fileSecurity = new PdfFileSecurity();

            // Bind the source PDF file to the facade.
            fileSecurity.BindPdf(inputPath);

            // Encrypt the file using the specified key size and algorithm.
            // User password: "userpass", Owner password: "ownerpass".
            bool success = fileSecurity.EncryptFile(
                userPassword: "userpass",
                ownerPassword: "ownerpass",
                privilege: s.Privilege,
                keySize: s.Key,
                cipher: s.Algo);

            if (!success)
            {
                // If encryption failed, report the underlying exception.
                Console.Error.WriteLine($"Failed to encrypt {s.Output}: {fileSecurity.LastException?.Message}");
                continue;
            }

            // Save the encrypted PDF to the designated output file.
            fileSecurity.Save(s.Output);
            Console.WriteLine($"Created encrypted file: {s.Output} (KeySize={s.Key}, Algorithm={s.Algo})");
        }

        Console.WriteLine("All encryption examples completed.");
    }
}