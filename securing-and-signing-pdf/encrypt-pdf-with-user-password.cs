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
        using (Document doc = new Document(inputStream))
        {
            // Define desired permissions (example: allow printing and content extraction).
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document. Owner password is left empty (can be set to a different value if needed).
            doc.Encrypt(userPassword, string.Empty, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF to an output MemoryStream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

// Added entry point to satisfy the compiler for an executable project.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfEncryptionHelper.
    }
}