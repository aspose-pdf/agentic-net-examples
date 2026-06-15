using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfApi
{
    public class PdfEncryptionHelper
    {
        /// <summary>
        /// Reads a PDF from the provided input stream, encrypts it with the given passwords,
        /// and writes the encrypted PDF to the provided output stream.
        /// </summary>
        /// <param name="inputPdfStream">Stream containing the source PDF (must be readable).</param>
        /// <param name="outputPdfStream">Stream where the encrypted PDF will be written (must be writable).</param>
        /// <param name="userPassword">Password required to open the PDF (user password).</param>
        /// <param name="ownerPassword">Password that grants full permissions (owner password).</param>
        public void EncryptPdfStream(Stream inputPdfStream, Stream outputPdfStream,
                                     string userPassword, string ownerPassword)
        {
            if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
            if (outputPdfStream == null) throw new ArgumentNullException(nameof(outputPdfStream));
            if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
            if (ownerPassword == null) throw new ArgumentNullException(nameof(ownerPassword));

            // Load the PDF from the input stream.
            using (Document pdfDoc = new Document(inputPdfStream))
            {
                // Define permissions – allow printing and content extraction.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using the recommended AES-256 algorithm.
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF to the output stream.
                pdfDoc.Save(outputPdfStream);
            }
        }
    }

    // Minimal entry point required for a console‑type project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Placeholder – no functional code needed for compilation.
            // Example usage (commented out):
            // using var input = File.OpenRead("input.pdf");
            // using var output = File.Create("output.pdf");
            // new PdfEncryptionHelper().EncryptPdfStream(input, output, "userPwd", "ownerPwd");
        }
    }
}