using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and final encrypted PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string encryptedPptxPath = "output_protected.pptx";

        // Password to protect the PPTX file
        const string password = "StrongPassword123";

        // ------------------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf core API
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
            {
                // Save as PPTX – must pass a PptxSaveOptions instance
                Aspose.Pdf.PptxSaveOptions pptxOpts = new Aspose.Pdf.PptxSaveOptions();
                pdfDoc.Save(pptxPath, pptxOpts);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑PPTX conversion: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // 2. Protect the generated PPTX with a password (AES‑256 encryption)
        // ------------------------------------------------------------
        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"PPTX file not found for encryption: {pptxPath}");
            return;
        }

        try
        {
            // Read the PPTX bytes
            byte[] plainBytes = File.ReadAllBytes(pptxPath);

            // Generate a random salt
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256‑bit key and a 128‑bit IV from the password and salt
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] key = pdb.GetBytes(32); // 256 bits
                byte[] iv  = pdb.GetBytes(16); // 128 bits

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = key;
                    aes.IV = iv;

                    using (MemoryStream cipherStream = new MemoryStream())
                    {
                        // Write salt and IV at the beginning of the file for later decryption
                        cipherStream.Write(salt, 0, salt.Length);
                        cipherStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new CryptoStream(cipherStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        }

                        // Write the encrypted content to the output file
                        File.WriteAllBytes(encryptedPptxPath, cipherStream.ToArray());
                    }
                }
            }

            Console.WriteLine($"PPTX file encrypted and saved as: {encryptedPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PPTX encryption: {ex.Message}");
        }
    }
}