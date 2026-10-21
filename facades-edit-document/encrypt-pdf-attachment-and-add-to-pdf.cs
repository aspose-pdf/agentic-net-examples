using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Encrypts raw data with a password using AES (CBC, 256‑bit key).
    // Returns the encrypted byte array (IV + ciphertext).
    static byte[] EncryptData(byte[] plainData, string password)
    {
        // Derive a 256‑bit key from the password (PBKDF2).
        using (var kdf = new Rfc2898DeriveBytes(password, 16, 10000))
        {
            byte[] key = kdf.GetBytes(32); // 256‑bit key
            byte[] iv  = kdf.Salt;        // use the generated salt as IV (16 bytes)

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    // Prepend IV so it can be used for decryption later.
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainData, 0, plainData.Length);
                    }
                    return ms.ToArray();
                }
            }
        }
    }

    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "secret.docx";
        const string outputPdfPath     = "output.pdf";

        // Password used to encrypt the attachment.
        const string attachmentPassword = "AttachPass123";

        // Optional: passwords for PDF encryption.
        const string pdfUserPassword   = "UserPass123";
        const string pdfOwnerPassword  = "OwnerPass123";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the existing PDF.
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // ------------------------------------------------------------
                // Encrypt the attachment file with the supplied password.
                // ------------------------------------------------------------
                byte[] plainAttachment = File.ReadAllBytes(attachmentPath);
                byte[] encryptedAttachment = EncryptData(plainAttachment, attachmentPassword);

                // Create a FileSpecification for the encrypted attachment.
                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(
                    Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(encryptedAttachment);

                // Add the encrypted file to the PDF's EmbeddedFiles collection.
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // ------------------------------------------------------------
                // (Optional) Encrypt the PDF itself for added security.
                // ------------------------------------------------------------
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                                Aspose.Pdf.Permissions.ExtractContent;
                pdfDoc.Encrypt(pdfUserPassword, pdfOwnerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with encrypted attachment: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}