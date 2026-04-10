using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the original PDF. Must be readable and seekable.</param>
    /// <param name="userPassword">User password (can be null or empty).</param>
    /// <param name="ownerPassword">Owner password (can be null or empty; a random one will be generated if null).</param>
    /// <returns>A MemoryStream positioned at the beginning, containing the encrypted PDF.</returns>
    public static Stream EncryptPdfStreamRc440(Stream inputPdf, string userPassword, string ownerPassword)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (!inputPdf.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(inputPdf));

        // Ensure the input stream is positioned at the start.
        if (inputPdf.CanSeek) inputPdf.Position = 0;

        // Prepare the output stream that will hold the encrypted PDF.
        MemoryStream encryptedStream = new MemoryStream();

        // Use the PdfFileSecurity facade to bind, encrypt, and save the PDF.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF stream.
            security.BindPdf(inputPdf);

            // Encrypt using RC4 with a 40‑bit key.
            // DocumentPrivilege can be set according to required permissions; here we allow printing.
            security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x40, Algorithm.RC4);

            // Save the encrypted PDF into the output stream.
            security.Save(encryptedStream);
        }

        // Reset the output stream position so the caller can read from the beginning.
        encryptedStream.Position = 0;
        return encryptedStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // No operation – the library class can be used by other code.
    }
}