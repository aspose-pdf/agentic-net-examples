using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it with the specified user password,
    /// and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Original PDF content.</param>
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Owner password (optional, can be empty).</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword = "")
    {
        // Wrap the input bytes in a MemoryStream for Aspose.Pdf to read.
        using (var inputStream = new MemoryStream(pdfBytes))
        {
            // Load the PDF directly from the stream using the Document constructor (no Facades).
            var doc = new Document(inputStream);

            // Define desired permissions (example: allow printing and content extraction).
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using the recommended CryptoAlgorithm (AES‑256).
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted document to a new MemoryStream.
            using (var outputStream = new MemoryStream())
            {
                doc.Save(outputStream);          // Save to stream (preserves PDF format).
                return outputStream.ToArray();   // Return the encrypted bytes.
            }
        }
    }

    // Dummy entry point to satisfy the compiler when this file is compiled as a console app.
    public static void Main()
    {
        // No operation – the class is intended to be used as a library.
    }
}
