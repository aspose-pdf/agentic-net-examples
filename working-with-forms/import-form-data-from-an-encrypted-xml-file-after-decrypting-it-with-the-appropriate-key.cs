using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    // Decrypts an AES‑encrypted XML file.
    // The first 16 bytes of the encrypted file are assumed to be the IV.
    // The password is used to derive a 256‑bit key via PBKDF2.
    private static MemoryStream DecryptXml(string encryptedFilePath, string password)
    {
        // Read the entire encrypted file.
        byte[] encryptedData = File.ReadAllBytes(encryptedFilePath);
        if (encryptedData.Length < 16)
            throw new InvalidDataException("Encrypted file is too short to contain an IV.");

        // Extract IV (first 16 bytes) and ciphertext.
        byte[] iv = new byte[16];
        Array.Copy(encryptedData, 0, iv, 0, 16);
        byte[] cipherText = new byte[encryptedData.Length - 16];
        Array.Copy(encryptedData, 16, cipherText, 0, cipherText.Length);

        // Derive a 256‑bit key from the password.
        using (var pdb = new Rfc2898DeriveBytes(password, iv, 10000))
        {
            byte[] key = pdb.GetBytes(32); // 256 bits

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream msCipher = new MemoryStream(cipherText))
                using (CryptoStream cs = new CryptoStream(msCipher, decryptor, CryptoStreamMode.Read))
                {
                    MemoryStream decryptedStream = new MemoryStream();
                    cs.CopyTo(decryptedStream);
                    decryptedStream.Position = 0; // reset for reading
                    return decryptedStream;
                }
            }
        }
    }

    static void Main()
    {
        const string inputPdfPath      = "FormTemplate.pdf";      // PDF with AcroForm fields
        const string outputPdfPath     = "FormWithData.pdf";      // Resulting PDF
        const string encryptedXmlPath  = "FormData.xml.enc";      // Encrypted XML containing form data
        const string decryptionPassword = "StrongPassword123";    // Password used for encryption

        // Verify input files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(encryptedXmlPath))
        {
            Console.Error.WriteLine($"Encrypted XML not found: {encryptedXmlPath}");
            return;
        }

        try
        {
            // Decrypt the XML into a memory stream.
            using (MemoryStream decryptedXml = DecryptXml(encryptedXmlPath, decryptionPassword))
            {
                // Initialize the Form facade with source and destination PDFs.
                using (Form form = new Form(inputPdfPath, outputPdfPath))
                {
                    // Import the decrypted XML data into the PDF form fields.
                    form.ImportXml(decryptedXml);

                    // Save the updated PDF.
                    form.Save();
                }
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}