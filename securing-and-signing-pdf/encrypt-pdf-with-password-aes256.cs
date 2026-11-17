using System;
using System.IO;
using Aspose.Pdf;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it with the specified passwords,
    /// and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Original PDF content.</param>
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Password that grants full permissions.</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        // Load the PDF from the input byte array using a MemoryStream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document document = new Document(inputStream))
        {
            // Define desired permissions (example: allow printing and content extraction).
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using AES-256 algorithm (recommended).
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted document to an output MemoryStream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                document.Save(outputStream);
                // Return the encrypted PDF bytes.
                return outputStream.ToArray();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Simple demonstration of the helper. If file paths are supplied via command line, use them;
        // otherwise fall back to default names.
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        byte[] originalBytes = File.ReadAllBytes(inputPath);
        string userPassword = "user123";   // example user password
        string ownerPassword = "owner123"; // example owner password

        byte[] encryptedBytes = PdfEncryptionHelper.EncryptPdf(originalBytes, userPassword, ownerPassword);
        File.WriteAllBytes(outputPath, encryptedBytes);
        Console.WriteLine($"Encrypted PDF saved to {outputPath}");
    }
}
