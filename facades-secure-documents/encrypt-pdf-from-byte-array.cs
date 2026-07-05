using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Loads a PDF from a byte array, encrypts it with the specified user and owner passwords,
        /// and returns the encrypted PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <param name="userPassword">Password required for opening the PDF (can be null or empty).</param>
        /// <param name="ownerPassword">Owner password that controls permissions (can be null or empty).</param>
        /// <returns>Encrypted PDF as a byte array.</returns>
        public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
        {
            // Load the PDF from the input byte array using the Document(Stream) constructor.
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            using (Document document = new Document(inputStream))
            {
                // Initialize the PdfFileSecurity facade with the loaded document.
                PdfFileSecurity security = new PdfFileSecurity(document);

                // Apply security settings. DocumentPrivilege.Print is used as an example; adjust privileges as needed.
                security.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);

                // Save the encrypted document to an output memory stream.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    document.Save(outputStream);
                    // Return the encrypted PDF bytes.
                    return outputStream.ToArray();
                }
            }
        }
    }

    // Dummy entry point to satisfy the console‑application requirement of the project.
    // The method does nothing; it merely allows the assembly to compile.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is accessed via PdfEncryptionHelper.
        }
    }
}
