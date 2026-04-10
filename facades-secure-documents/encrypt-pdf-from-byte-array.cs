using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it, and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="userPassword">User password (can be null or empty).</param>
    /// <param name="ownerPassword">Owner password (can be null or empty).</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Input stream for the original PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Output stream for the encrypted PDF
        using (MemoryStream outputStream = new MemoryStream())
        // PdfFileSecurity facade to perform encryption
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the source PDF stream to the facade
            fileSecurity.BindPdf(inputStream);

            // Encrypt the PDF.
            // DocumentPrivilege.Print is used as an example; adjust as needed.
            // KeySize.x256 provides strong AES‑256 encryption.
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

// Dummy entry point required for a console‑type project build.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via PdfEncryptionHelper.
    }
}