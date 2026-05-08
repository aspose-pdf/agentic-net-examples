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
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Owner password (can be the same as userPassword).</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        // Load the PDF from the input byte array using a MemoryStream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document document = new Document(inputStream))
        {
            // Define desired permissions (example: allow printing only).
            Permissions permissions = Permissions.PrintDocument;

            // Encrypt the document with AES-256 algorithm.
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF into a MemoryStream and return its byte array.
            using (MemoryStream outputStream = new MemoryStream())
            {
                document.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

// Entry point required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation needed – the class library functionality is accessed via PdfEncryptionHelper.
        // This method exists solely to satisfy the compiler's requirement for a static Main entry point.
    }
}