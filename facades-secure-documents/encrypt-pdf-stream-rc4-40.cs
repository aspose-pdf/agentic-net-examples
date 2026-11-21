using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Example usage: encrypt a PDF file using RC4‑40.
        string inputPath = "sample.pdf";
        string outputPath = "sample_encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (FileStream inputStream = File.OpenRead(inputPath))
        {
            Stream encrypted = PdfEncryptionHelper.EncryptPdfStream(inputStream, "user123", "owner123");
            using (FileStream outStream = File.Create(outputPath))
            {
                encrypted.CopyTo(outStream);
            }
            encrypted.Dispose();
        }

        Console.WriteLine($"Encrypted PDF saved to {outputPath}");
    }
}

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF provided as a stream using RC4‑40 encryption and returns the encrypted PDF as a new stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the original PDF. Must be readable and seekable.</param>
    /// <param name="userPassword">Password required to open the encrypted PDF.</param>
    /// <param name="ownerPassword">Owner password that grants full permissions.</param>
    /// <returns>A MemoryStream containing the encrypted PDF. Caller is responsible for disposing the returned stream.</returns>
    public static Stream EncryptPdfStream(Stream inputPdf, string userPassword, string ownerPassword)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (!inputPdf.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(inputPdf));
        if (!inputPdf.CanSeek) throw new ArgumentException("Input stream must be seekable.", nameof(inputPdf));

        // Ensure the stream is positioned at the beginning.
        inputPdf.Position = 0;

        // Load the PDF document from the provided stream.
        using (Document doc = new Document(inputPdf))
        {
            // Define permissions for the encrypted document.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply RC4‑40 encryption.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x40);

            // Save the encrypted PDF into a memory stream.
            MemoryStream encryptedStream = new MemoryStream();
            doc.Save(encryptedStream);
            encryptedStream.Position = 0; // Reset for reading by the caller.
            return encryptedStream;
        }
    }
}