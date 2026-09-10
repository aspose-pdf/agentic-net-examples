using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Provides PDF encryption utilities.
    /// </summary>
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
        /// No user or owner passwords are set (empty strings), and the document privilege is set to allow printing.
        /// </summary>
        /// <param name="inputPdf">Stream containing the source PDF. The stream must remain open for the duration of the call.</param>
        /// <returns>A MemoryStream containing the encrypted PDF. The caller is responsible for disposing the returned stream.</returns>
        public static Stream EncryptPdfStreamRc440(Stream inputPdf)
        {
            if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));

            // Output stream that will hold the encrypted PDF
            MemoryStream encryptedStream = new MemoryStream();

            // PdfFileSecurity implements IDisposable via SaveableFacade, so use a using block
            using (PdfFileSecurity securityFacade = new PdfFileSecurity())
            {
                // Bind the source PDF stream to the facade
                securityFacade.BindPdf(inputPdf);

                // Apply RC4‑40 encryption.
                // No passwords are set (empty strings). Privilege is set to allow printing.
                // KeySize.x40 selects 40‑bit key, Algorithm.RC4 selects the RC4 cipher.
                securityFacade.EncryptFile(
                    userPassword: string.Empty,
                    ownerPassword: string.Empty,
                    privilege: DocumentPrivilege.Print,
                    keySize: KeySize.x40,
                    cipher: Algorithm.RC4);

                // Save the encrypted document into the output stream
                securityFacade.Save(encryptedStream);
            }

            // Reset position so the caller can read from the beginning
            encryptedStream.Position = 0;
            return encryptedStream;
        }
    }

    // Minimal entry point required for a console‑type project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Placeholder Main – the project builds as an executable.
            // Real usage can be added here or the assembly can be referenced from another project.
        }
    }
}
