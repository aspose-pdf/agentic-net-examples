using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string originalAttachment = "secret.txt";
        const string encryptedAttachment = "secret.enc";
        const string outputPdf = "output_with_attachment.pdf";
        const string description = "Encrypted attachment (AES-256)";

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Placeholder PDF – only works on Windows because Aspose.Pdf uses GDI+ for rendering.
            // On non‑Windows platforms you would need a pre‑existing PDF.
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                var placeholder = new Document();
                placeholder.Pages.Add();
                placeholder.Save(inputPdf);
                Console.WriteLine($"Placeholder PDF created at '{inputPdf}'.");
            }
            else
            {
                Console.WriteLine($"Input PDF '{inputPdf}' not found and cannot be created on this OS.");
                return;
            }
        }

        // ------------------------------------------------------------
        // Ensure the attachment file exists – create a dummy file if missing.
        // ------------------------------------------------------------
        if (!File.Exists(originalAttachment))
        {
            File.WriteAllText(originalAttachment, "This is a secret message that will be encrypted.");
            Console.WriteLine($"Dummy attachment created at '{originalAttachment}'.");
        }

        // Demo key/IV – replace with secure generation in production
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv  = new byte[16]; // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++)  iv[i] = (byte)(i + 1);

        // Encrypt the file to be attached
        EncryptFile(originalAttachment, encryptedAttachment, key, iv);

        // Load the source PDF, add the encrypted file as an embedded file, and save
        Document pdfDoc = new Document(inputPdf);

        // Create a FileSpecification for the encrypted attachment
        var fileSpec = new FileSpecification(Path.GetFileName(encryptedAttachment), description);
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(encryptedAttachment));
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        pdfDoc.Save(outputPdf);

        // Clean‑up temporary encrypted file
        if (File.Exists(encryptedAttachment))
            File.Delete(encryptedAttachment);

        Console.WriteLine($"Attachment added. Output saved to '{outputPdf}'.");
    }

    static void EncryptFile(string inputPath, string outputPath, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (FileStream inStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream crypto = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inStream.CopyTo(crypto);
            }
        }
    }
}
