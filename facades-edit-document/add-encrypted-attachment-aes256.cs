using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentFilePath = "secret.txt";   // file to encrypt and attach
        const string outputPdfPath = "output.pdf";        // result PDF
        const string description = "Encrypted secret file"; // custom description
        const string password = "StrongPassword123";      // password for AES encryption

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Create a temporary file to hold the encrypted attachment
        string tempEncryptedPath = Path.GetTempFileName();

        try
        {
            // ---------- Encrypt the attachment using AES‑256 ----------
            // Generate a random 16‑byte salt
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            // Derive a 256‑bit key and a 128‑bit IV from the password using the static Pbkdf2 method
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                32,                     // 256‑bit key
                HashAlgorithmName.SHA256,
                10000);

            byte[] iv = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                16,                     // 128‑bit IV
                HashAlgorithmName.SHA256,
                10000);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Write the salt at the beginning of the encrypted file (needed for decryption)
                using (FileStream outStream = new FileStream(tempEncryptedPath, FileMode.Create, FileAccess.Write))
                {
                    outStream.Write(salt, 0, salt.Length);
                    using (CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (FileStream inStream = new FileStream(attachmentFilePath, FileMode.Open, FileAccess.Read))
                    {
                        inStream.CopyTo(cryptoStream);
                    }
                }
            }

            // ---------- Prepare the output PDF ----------
            // Load the source PDF, add the encrypted attachment, then save as the output PDF
            Document pdfDoc = new Document(inputPdfPath);

            // Create a FileSpecification for the encrypted attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
            {
                Description = description,
                // The encrypted content is stored in the Contents stream
                Contents = new MemoryStream(File.ReadAllBytes(tempEncryptedPath))
            };

            // Add the attachment to the PDF's EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Attachment added successfully. Output PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary encrypted file
            if (File.Exists(tempEncryptedPath))
            {
                try { File.Delete(tempEncryptedPath); } catch { /* ignore */ }
            }
        }
    }
}
