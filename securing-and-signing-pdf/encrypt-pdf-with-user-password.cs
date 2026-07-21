using System;
using System.IO;
using Aspose.Pdf;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it with the specified user password,
    /// and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="userPassword">The password required to open the encrypted PDF.</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword)
    {
        // Load the source PDF from the input byte array.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Define desired permissions (example: allow printing and content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document.
            // Owner password is set to the same value as the user password for simplicity.
            doc.Encrypt(userPassword, userPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF into a memory stream and return its bytes.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Ensure the stream position is at the beginning before reading.
                outputStream.Position = 0;
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the console‑application build configuration.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfEncryptionHelper.
        // Example usage (commented out):
        // byte[] source = File.ReadAllBytes("sample.pdf");
        // byte[] encrypted = PdfEncryptionHelper.EncryptPdf(source, "myPassword");
        // File.WriteAllBytes("encrypted.pdf", encrypted);
    }
}