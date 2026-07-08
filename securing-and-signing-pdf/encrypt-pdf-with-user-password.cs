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
        // Load the PDF from the input byte array using a MemoryStream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Create a Document instance from the stream (lifecycle rule: use constructor).
        using (Document doc = new Document(inputStream))
        {
            // Define desired permissions (example: allow printing and content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document.
            // Owner password is left empty; CryptoAlgorithm.AESx256 is the recommended algorithm.
            doc.Encrypt(userPassword, string.Empty, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted document to a new MemoryStream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream); // Save without SaveOptions writes PDF regardless of extension.
                return outputStream.ToArray(); // Return the encrypted PDF bytes.
            }
        }
    }
}

public class Program
{
    // Entry point required for a console‑application project.
    public static void Main(string[] args)
    {
        // The Main method can remain empty or contain demo code.
        // Example (commented out) showing how to use the helper:
        // byte[] originalPdf = File.ReadAllBytes("input.pdf");
        // byte[] encryptedPdf = PdfEncryptionHelper.EncryptPdf(originalPdf, "myPassword");
        // File.WriteAllBytes("encrypted.pdf", encryptedPdf);
    }
}
