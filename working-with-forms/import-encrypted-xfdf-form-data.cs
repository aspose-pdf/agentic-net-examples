using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";                 // PDF with interactive form (created inline)
        const string encryptedXmlPath = "data.xml.enc";    // Encrypted XFDF file (created inline)
        const string outputPdfPath = "form_filled.pdf";    // Resulting PDF

        // Example key/IV – deterministic values for the demo
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv  = new byte[16]; // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++)  iv[i]  = (byte)(i + 1);

        // ------------------------------------------------------------
        // 1. Create a simple PDF containing a single text box field.
        // ------------------------------------------------------------
        CreatePdfTemplate(pdfPath);

        // ------------------------------------------------------------
        // 2. Build an XFDF string that fills the field and encrypt it.
        // ------------------------------------------------------------
        string xfdf = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                      "<xfdf xmlns=\"http://ns.adobe.com/xfdf/\">\n" +
                      "  <fields>\n" +
                      "    <field name=\"FullName\">\n" +
                      "      <value>John Doe</value>\n" +
                      "    </field>\n" +
                      "  </fields>\n" +
                      "</xfdf>";
        EncryptAndSave(xfdf, encryptedXmlPath, key, iv);

        // ------------------------------------------------------------
        // 3. Decrypt the XFDF and import the values into the PDF.
        // ------------------------------------------------------------
        using (FileStream encStream = File.OpenRead(encryptedXmlPath))
        using (MemoryStream decryptedStream = DecryptStream(encStream, key, iv))
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Import field values from the decrypted XFDF stream
            XfdfReader.ReadFields(decryptedStream, pdfDoc);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    // Creates a PDF with a single TextBoxField named "FullName".
    private static void CreatePdfTemplate(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Define a rectangle for the field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            var rect = new Rectangle(100, 700, 300, 720);
            TextBoxField txt = new TextBoxField(page, rect)
            {
                PartialName = "FullName",
                Value = string.Empty
            };
            doc.Form.Add(txt);
            doc.Save(path);
        }
    }

    // Encrypts a plain‑text string using AES‑CBC and writes the ciphertext to a file.
    private static void EncryptAndSave(string plainText, string outputPath, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter writer = new StreamWriter(cryptoStream))
            {
                writer.Write(plainText);
            }
        }
    }

    // Decrypts an input stream using AES‑CBC and returns a MemoryStream with plaintext.
    private static MemoryStream DecryptStream(Stream encryptedStream, byte[] key, byte[] iv)
    {
        encryptedStream.Position = 0; // Ensure start position

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                MemoryStream plainStream = new MemoryStream();
                cryptoStream.CopyTo(plainStream);
                plainStream.Position = 0; // Reset for reading
                return plainStream;
            }
        }
    }
}
