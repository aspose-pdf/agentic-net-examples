using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required as per task specification

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF. The stream must be readable and positioned at the beginning.</param>
    /// <param name="userPassword">Password required to open the PDF for reading.</param>
    /// <param name="ownerPassword">Password that grants full permissions (owner).</param>
    /// <returns>A MemoryStream containing the encrypted PDF. Caller is responsible for disposing the returned stream.</returns>
    public static Stream EncryptPdfStream(Stream inputPdf, string userPassword, string ownerPassword)
    {
        // Ensure the input stream is at the beginning
        if (inputPdf.CanSeek)
            inputPdf.Position = 0;

        // Load the PDF from the input stream inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define permissions – adjust as needed; here we allow printing and content extraction
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply RC4‑40 encryption using the CryptoAlgorithm enum
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x40);

            // Save the encrypted PDF to a memory stream
            MemoryStream encryptedStream = new MemoryStream();
            doc.Save(encryptedStream);

            // Reset position so the caller can read from the beginning
            encryptedStream.Position = 0;
            return encryptedStream;
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is exposed via PdfEncryptionHelper.
    }
}
