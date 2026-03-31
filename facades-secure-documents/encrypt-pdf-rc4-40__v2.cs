using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfEncryptionHelper
{
    public static MemoryStream EncryptPdfStream(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning
        inputPdfStream.Position = 0;

        // Stream that will hold the encrypted PDF
        var encryptedStream = new MemoryStream();

        // Use the PdfFileSecurity facade to apply RC4‑40 encryption
        using (var security = new PdfFileSecurity())
        {
            // Load the PDF from the provided stream
            security.BindPdf(inputPdfStream);

            // Encrypt with RC4 40‑bit key, no user password, an owner password, and allow printing
            security.EncryptFile(
                "",                     // user password (empty)
                "ownerPass",            // owner password
                DocumentPrivilege.Print, // privilege (example: allow printing)
                KeySize.x40,             // 40‑bit key size
                Algorithm.RC4            // RC4 algorithm
            );

            // Save the encrypted PDF into the output stream
            security.Save(encryptedStream);
        }

        // Reset the position of the output stream before returning
        encryptedStream.Position = 0;
        return encryptedStream;
    }
}

// Dummy entry point required when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via PdfEncryptionHelper.
    }
}