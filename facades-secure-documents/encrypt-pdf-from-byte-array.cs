using System;
using System.IO;
using Aspose.Pdf;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it using AES‑256, and returns the encrypted PDF as a byte array.
    /// The Aspose.Pdf.Document class is used for the encryption operation.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Password that grants full permissions.</param>
    /// <returns>Encrypted PDF as a byte array.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        // Load the PDF directly from the supplied byte array using a MemoryStream.
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var doc = new Document(inputStream))
        {
            // Define the permissions that will be allowed after encryption.
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document with AES‑256.
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF back into a MemoryStream and return the bytes.
            using (var outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Example usage – read a PDF file, encrypt it, and write the encrypted version.
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        byte[] originalBytes = File.ReadAllBytes(inputPath);
        byte[] encryptedBytes = PdfEncryptionHelper.EncryptPdf(originalBytes, "user123", "owner123");
        File.WriteAllBytes(outputPath, encryptedBytes);
        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}