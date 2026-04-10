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
    /// <param name="userPassword">Password required to open the encrypted PDF.</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));

        // Load the PDF from the input byte array using DocumentFactory (instance method).
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            var factory = new DocumentFactory();
            using (Document doc = factory.CreateDocument(inputStream))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document. Owner password can be the same as user password or a different one.
                // CryptoAlgorithm.AESx256 is the recommended algorithm.
                doc.Encrypt(userPassword, userPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF to a memory stream and return the byte array.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    doc.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfEncryptionHelper.
    }
}