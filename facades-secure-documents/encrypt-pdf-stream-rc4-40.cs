using System;
using System.IO;
using Aspose.Pdf; // Document, Permissions, CryptoAlgorithm

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF provided as a stream using RC4‑40 encryption.
    /// The method returns a new stream containing the encrypted PDF.
    /// </summary>
    /// <param name="inputPdfStream">Stream with the original PDF (must be readable).</param>
    /// <param name="userPassword">Password required to open the PDF (can be empty).</param>
    /// <param name="ownerPassword">Owner password (can be empty; a random one will be generated if empty).</param>
    /// <returns>A MemoryStream containing the encrypted PDF. Caller is responsible for disposing it.</returns>
    public static Stream EncryptPdfStream(Stream inputPdfStream, string userPassword, string ownerPassword)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (!inputPdfStream.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream that will hold the encrypted PDF.
        MemoryStream encryptedStream = new MemoryStream();

        // Load the PDF from the input stream, apply encryption, and save to the output stream.
        using (Document doc = new Document(inputPdfStream))
        {
            // Define desired permissions (example: allow printing only).
            Permissions perms = Permissions.PrintDocument;

            // Apply RC4‑40 encryption.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x40);

            // Save the encrypted document to the memory stream.
            doc.Save(encryptedStream);
        }

        // Reset the position of the output stream so it can be read from the start.
        encryptedStream.Position = 0;
        return encryptedStream;
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
