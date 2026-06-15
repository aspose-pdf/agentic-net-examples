using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF supplied as a byte array and returns the encrypted PDF as a byte array.
    /// Uses Aspose.Pdf.Facades.PdfFileSecurity for encryption.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="userPassword">User password (can be null or empty).</param>
    /// <param name="ownerPassword">Owner password (can be null or empty).</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Input stream from the original PDF bytes
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Output stream that will hold the encrypted PDF
        using (MemoryStream outputStream = new MemoryStream())
        // PdfFileSecurity facade for encryption
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the PDF document to the facade
            fileSecurity.BindPdf(inputStream);

            // Encrypt the PDF.
            // DocumentPrivilege.Print is used as an example; adjust as needed.
            // KeySize.x256 provides strong AES-256 encryption.
            fileSecurity.EncryptFile(
                userPassword ?? string.Empty,
                ownerPassword ?? string.Empty,
                DocumentPrivilege.Print,
                KeySize.x256);

            // Save the encrypted PDF to the output stream
            fileSecurity.Save(outputStream);

            // Return the encrypted bytes
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
// This pattern is useful when the source file is intended to be used as a library but the
// project type cannot be changed. The Main method does nothing and simply exits.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods are intended to be called by other code.
    }
}
