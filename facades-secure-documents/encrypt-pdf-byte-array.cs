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
    /// <returns>Encrypted PDF as a byte array.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Input stream wrapping the source PDF bytes
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Output stream that will receive the encrypted PDF
        using (MemoryStream outputStream = new MemoryStream())
        // PdfFileSecurity facade for encryption
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the source PDF from the input stream
            fileSecurity.BindPdf(inputStream);

            // Choose desired privileges and key size.
            // DocumentPrivilege.Print allows printing; adjust as needed.
            // KeySize.x256 provides strong AES‑256 encryption.
            bool success = fileSecurity.EncryptFile(
                userPassword ?? string.Empty,
                ownerPassword ?? string.Empty,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (!success)
                throw new InvalidOperationException("PDF encryption failed.");

            // Save the encrypted PDF into the output stream
            fileSecurity.Save(outputStream);

            // Return the encrypted bytes
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // This method is intentionally left empty.
        // It can be used for quick manual testing, e.g.:
        // byte[] source = File.ReadAllBytes("input.pdf");
        // byte[] encrypted = PdfEncryptionHelper.EncryptPdf(source, "user", "owner");
        // File.WriteAllBytes("encrypted.pdf", encrypted);
    }
}