using System;
using System.IO;
using Aspose.Pdf;                 // DocumentPrivilege enum
using Aspose.Pdf.Facades;        // PdfFileSecurity, KeySize, Algorithm

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the original PDF. The stream must be readable and seekable.</param>
    /// <returns>A MemoryStream containing the encrypted PDF. The returned stream is positioned at the beginning.</returns>
    public static Stream EncryptPdfStreamRc440(Stream inputPdfStream)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (!inputPdfStream.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(inputPdfStream));
        if (!inputPdfStream.CanSeek) throw new ArgumentException("Input stream must be seekable.", nameof(inputPdfStream));

        // Ensure the input stream is positioned at the start.
        inputPdfStream.Position = 0;

        // Prepare an output stream to hold the encrypted PDF.
        MemoryStream encryptedStream = new MemoryStream();

        // Use PdfFileSecurity (Facades API) to apply encryption.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF stream.
            security.BindPdf(inputPdfStream);

            // Apply RC4‑40 encryption.
            // Empty strings for passwords mean no password is required to open the document.
            // DocumentPrivilege.Print is used as an example; adjust as needed.
            security.EncryptFile(
                userPassword: string.Empty,
                ownerPassword: string.Empty,
                privilege: DocumentPrivilege.Print,
                keySize: KeySize.x40,
                cipher: Algorithm.RC4);

            // Save the encrypted PDF into the output stream.
            security.Save(encryptedStream);
        }

        // Reset the position so the caller can read from the beginning.
        encryptedStream.Position = 0;
        return encryptedStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank. The library functionality is accessed via PdfEncryptionHelper.
    }
}