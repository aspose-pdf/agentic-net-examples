using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF, encrypts it, and returns the encrypted content in a MemoryStream.
    /// The stream is positioned at the beginning and can be sent over a network.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Owner password with full permissions.</param>
    /// <returns>MemoryStream containing the encrypted PDF.</returns>
    public static MemoryStream GetEncryptedPdfStream(string inputPdfPath, string userPassword, string ownerPassword)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Permissions to allow printing and content extraction; adjust as needed.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // MemoryStream will hold the encrypted PDF.
        MemoryStream encryptedStream = new MemoryStream();

        // Load the source PDF using the Document class (the primary API).
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply encryption. AES‑256 is recommended for strong security.
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF directly into the MemoryStream.
            pdfDoc.Save(encryptedStream, SaveFormat.Pdf);
        }

        // Reset the stream position so that consumers can read from the beginning.
        encryptedStream.Position = 0;
        return encryptedStream; // Caller is responsible for disposing the stream.
    }

    // Example usage:
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string userPwd   = "user123";
        const string ownerPwd  = "owner123";

        try
        {
            using (MemoryStream stream = GetEncryptedPdfStream(sourcePdf, userPwd, ownerPwd))
            {
                Console.WriteLine($"Encrypted PDF size: {stream.Length} bytes");
                // Optional: write to a file for verification.
                File.WriteAllBytes("encrypted_output.pdf", stream.ToArray());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
