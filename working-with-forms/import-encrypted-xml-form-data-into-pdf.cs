using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the PDF form, the encrypted XML data, and the output PDF.
        const string pdfPath = "form.pdf";
        const string encryptedXmlPath = "data.xml.enc";
        const string outputPdfPath = "filled_form.pdf";

        // -----------------------------------------------------------------
        // Decrypt the XML file.
        // Replace the placeholder key/IV with the actual Base‑64 values used for encryption.
        // -----------------------------------------------------------------
        const string base64Key = "REPLACE_WITH_BASE64_KEY"; // e.g. "bW9ja0tleU1vcmVNb3Jl..."
        const string base64Iv  = "REPLACE_WITH_BASE64_IV";  // e.g. "bW9ja0lWbW9yZV..."

        if (!TryParseBase64(base64Key, out byte[] key) || !TryParseBase64(base64Iv, out byte[] iv))
        {
            Console.Error.WriteLine("[Error] The provided key or IV is not a valid Base‑64 string. " +
                                    "Please replace the placeholders with real values.");
            return;
        }

        byte[] decryptedXmlBytes;
        using (FileStream encryptedStream = File.OpenRead(encryptedXmlPath))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV  = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (CryptoStream cryptoStream = new CryptoStream(
                       encryptedStream,
                       aes.CreateDecryptor(),
                       CryptoStreamMode.Read))
            using (MemoryStream tempMs = new MemoryStream())
            {
                cryptoStream.CopyTo(tempMs);
                decryptedXmlBytes = tempMs.ToArray();
            }
        }

        // -----------------------------------------------------------------
        // Load the PDF document, bind the decrypted XML data to it,
        // and save the result.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        using (MemoryStream xmlStream = new MemoryStream(decryptedXmlBytes))
        {
            // Import form fields from the XML into the PDF.
            pdfDoc.BindXml(xmlStream);

            // Save the updated PDF with the imported form data.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Tries to convert a Base‑64 encoded string to a byte array.
    /// Returns false if the string is null, empty, still contains the placeholder text,
    /// or cannot be decoded.
    /// </summary>
    private static bool TryParseBase64(string base64, out byte[] bytes)
    {
        bytes = null;
        if (string.IsNullOrWhiteSpace(base64) ||
            base64.Contains("REPLACE_WITH_BASE64"))
        {
            return false;
        }
        try
        {
            bytes = Convert.FromBase64String(base64);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
