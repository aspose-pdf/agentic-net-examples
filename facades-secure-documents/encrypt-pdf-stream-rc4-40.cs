using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize, Algorithm

namespace AsposePdfApi
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
        /// </summary>
        /// <param name="inputPdf">Stream containing the original PDF. Must be readable.</param>
        /// <param name="userPassword">User password (can be null or empty).</param>
        /// <param name="ownerPassword">Owner password (can be null or empty).</param>
        /// <returns>A MemoryStream positioned at the beginning, containing the encrypted PDF.</returns>
        public static Stream EncryptPdfStreamRc440(Stream inputPdf, string userPassword, string ownerPassword)
        {
            // Ensure the input stream is positioned at the start.
            if (inputPdf.CanSeek)
                inputPdf.Position = 0;

            // Prepare an output stream that will hold the encrypted PDF.
            MemoryStream encryptedStream = new MemoryStream();

            // Use the PdfFileSecurity facade to apply encryption.
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the source PDF stream to the facade.
                security.BindPdf(inputPdf);

                // Encrypt with RC4‑40. DocumentPrivilege.Print is used as an example;
                // adjust the privilege as needed for your scenario.
                security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x40,
                    Algorithm.RC4);

                // Save the encrypted document into the output stream.
                security.Save(encryptedStream);
            }

            // Reset the output stream position so the caller can read from the beginning.
            encryptedStream.Position = 0;
            return encryptedStream;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The library can still be used from other projects; the Main method does nothing.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – placeholder for the required entry point.
        }
    }
}
