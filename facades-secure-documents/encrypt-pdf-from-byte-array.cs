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
    /// <param name="pdfBytes">Input PDF bytes.</param>
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
        // PdfFileSecurity facade handles encryption
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the source PDF from the input stream
            fileSecurity.BindPdf(inputStream);

            // Encrypt the PDF.
            // DocumentPrivilege.Print is an example; adjust as needed.
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

// ---------------------------------------------------------------------------
// The original project was compiled as an executable, which requires an entry
// point (a static Main method).  Adding a minimal Program class satisfies the
// compiler while keeping the helper class usable from other code.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – this method exists solely to provide an entry point
        // for the console application build.  The PdfEncryptionHelper can be
        // invoked from other projects or unit tests.
    }
}
