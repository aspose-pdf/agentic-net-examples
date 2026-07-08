using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEncryption
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a stream.
        /// </summary>
        /// <param name="inputPdf">Stream containing the original PDF.</param>
        /// <returns>MemoryStream with the RC4‑40 encrypted PDF.</returns>
        public static Stream EncryptPdfStreamRc440(Stream inputPdf)
        {
            if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));

            // Ensure the input stream is positioned at the beginning.
            if (inputPdf.CanSeek)
                inputPdf.Position = 0;

            // Load the PDF from the input stream.
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the security facade with the loaded document.
                PdfFileSecurity security = new PdfFileSecurity(doc);

                // Apply RC4‑40 encryption.
                // Empty strings are used for user and owner passwords.
                // DocumentPrivilege.Print is chosen arbitrarily; adjust as needed.
                security.EncryptFile(
                    userPassword: string.Empty,
                    ownerPassword: string.Empty,
                    privilege: DocumentPrivilege.Print,
                    keySize: KeySize.x40,
                    cipher: Algorithm.RC4);

                // Save the encrypted PDF into a memory stream.
                MemoryStream encryptedStream = new MemoryStream();
                security.Save(encryptedStream);

                // Close the facade (optional but good practice).
                security.Close();

                // Reset the position so the caller can read from the beginning.
                encryptedStream.Position = 0;
                return encryptedStream;
            }
        }
    }

    // Dummy entry point to satisfy the console‑application project type.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is accessed via PdfEncryptionHelper.
        }
    }
}