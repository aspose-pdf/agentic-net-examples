using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Reads a PDF from <paramref name="inputStream"/>, encrypts it with the specified passwords,
    /// and writes the encrypted PDF to <paramref name="outputStream"/>.
    /// </summary>
    /// <param name="inputStream">Stream containing the source PDF. Must be readable.</param>
    /// <param name="outputStream">Stream where the encrypted PDF will be written. Must be writable.</param>
    /// <param name="userPassword">User password (can be empty for no user password).</param>
    /// <param name="ownerPassword">Owner password (can be empty for no owner password).</param>
    public static void EncryptPdfStream(
        Stream inputStream,
        Stream outputStream,
        string userPassword,
        string ownerPassword)
    {
        if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));
        if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

        // The Document constructor with the 'isManagedStream' flag set to false
        // prevents Aspose.Pdf from closing the streams when the Document is disposed.
        using (Document doc = new Document(inputStream, false))
        {
            // Define the permissions you want to allow after encryption.
            // Here we allow printing and content extraction; adjust as needed.
            Permissions perms = Permissions.PrintDocument |
                                 Permissions.ExtractContent;

            // Encrypt the document using the recommended AES‑256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF directly to the output stream.
            // The Save method does NOT close the stream.
            doc.Save(outputStream);
        }
    }
}

// Minimal entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library can be used by referencing this assembly.
        // Example usage (commented out to avoid runtime errors when no streams are provided):
        // using (var input = File.OpenRead("input.pdf"))
        // using (var output = File.Create("output_encrypted.pdf"))
        // {
        //     PdfEncryptionHelper.EncryptPdfStream(input, output, "user123", "owner123");
        // }
    }
}
