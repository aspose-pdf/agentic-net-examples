using System;
using System.IO;
using System.Net.Sockets; // For NetworkStream
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, DocumentFactory, Permissions, CryptoAlgorithm)

namespace AsposePdfApi
{
    class PdfEncryptionHandler
    {
        /// <summary>
        /// Reads a PDF from the provided network stream, encrypts it, and writes the encrypted PDF back to the same stream.
        /// The method assumes the stream supports seeking (e.g., a buffered NetworkStream or a MemoryStream wrapper).
        /// </summary>
        /// <param name="networkStream">The network stream containing the original PDF data.</param>
        /// <param name="userPassword">Password required to open the encrypted PDF (user password).</param>
        /// <param name="ownerPassword">Password that grants full permissions (owner password).</param>
        public static void EncryptPdfOnStream(NetworkStream networkStream, string userPassword, string ownerPassword)
        {
            if (networkStream == null) throw new ArgumentNullException(nameof(networkStream));
            if (!networkStream.CanRead || !networkStream.CanWrite) throw new InvalidOperationException("Stream must support read and write.");

            // Ensure the stream position is at the beginning before reading.
            if (networkStream.CanSeek) networkStream.Position = 0;

            // Load the PDF from the incoming stream using the DocumentFactory lifecycle rule.
            // The created Document will not close the underlying stream when disposed (isManagedStream = false).
            Document pdfDoc;
            using (MemoryStream tempStream = new MemoryStream())
            {
                // Copy the network stream into a seekable MemoryStream because NetworkStream may not support seeking.
                networkStream.CopyTo(tempStream);
                tempStream.Position = 0;

                // Create the Document from the MemoryStream via an instance of DocumentFactory (non‑static API).
                var factory = new DocumentFactory();
                pdfDoc = factory.CreateDocument(tempStream);
            }

            // Apply encryption using the recommended CryptoAlgorithm (AESx256) as per the encryption rule.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF back into a new MemoryStream.
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                // Document disposal follows the document-disposal-with-using rule.
                using (pdfDoc)
                {
                    pdfDoc.Save(encryptedStream); // Save rule: Document.Save(Stream)
                }

                // Prepare the encrypted data for sending back over the network.
                encryptedStream.Position = 0;

                // Optionally reset the original network stream (if it supports seeking) before writing.
                if (networkStream.CanSeek) networkStream.SetLength(0);
                if (networkStream.CanSeek) networkStream.Position = 0;

                // Write encrypted PDF to the network stream.
                encryptedStream.CopyTo(networkStream);
                networkStream.Flush();
            }
        }
    }

    // Simple entry point to satisfy the compiler. In real usage the handler would be called from client code.
    class Program
    {
        static void Main(string[] args)
        {
            // Example placeholder – does nothing when run as a library.
            // To test the encryption handler, uncomment and provide a reachable TCP endpoint.
            //
            // TcpClient client = new TcpClient("example.com", 12345);
            // using (NetworkStream ns = client.GetStream())
            // {
            //     PdfEncryptionHandler.EncryptPdfOnStream(ns, "user123", "owner123");
            // }
        }
    }
}
