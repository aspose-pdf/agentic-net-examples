using System;
using System.IO;
using Aspose.Pdf;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Reads a PDF from <paramref name="inputStream"/>, encrypts it with the specified passwords,
    /// and writes the encrypted PDF to <paramref name="outputStream"/>.
    /// </summary>
    /// <param name="inputStream">Stream containing the source PDF (must be readable).</param>
    /// <param name="outputStream">Stream where the encrypted PDF will be written (must be writable).</param>
    /// <param name="userPassword">Password required to open the PDF for reading.</param>
    /// <param name="ownerPassword">Password that grants full permissions (owner).</param>
    public static void EncryptPdfStream(Stream inputStream, Stream outputStream, string userPassword, string ownerPassword)
    {
        if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));
        if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
        if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
        if (ownerPassword == null) throw new ArgumentNullException(nameof(ownerPassword));

        // Load the PDF from the input stream.
        // Using the Document constructor that accepts a Stream.
        using (Document pdfDoc = new Document(inputStream))
        {
            // Define the permissions you want to allow.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using AES-256 (recommended).
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF directly to the output stream.
            pdfDoc.Save(outputStream);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Placeholder entry point to satisfy the compiler.
        // No operation is performed here.
    }
}
